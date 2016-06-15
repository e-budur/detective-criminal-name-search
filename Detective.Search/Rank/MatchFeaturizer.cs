using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Data;
using Detective.Index.Trie;
using Detective.Query.Algorithms;
using Detective.Query.Result;

namespace Detective.Search.Rank
{
    public class MatchFeaturizer
    {
        public MatchFeatures Featurize(int uid, int nameIndex, CandidateTermSet termset)
        {
            var features = new MatchFeatures();
            features.CandidateTermSet = GetMaximalMatches(termset);

            features.Uid = uid;
            features.NameIndex = nameIndex;
            var allNameTerms = MemoryDB.GetAllNameTerms(uid, nameIndex);
            features.TotalInformation = allNameTerms.Sum(i => Math.Min(i.Tf, 5) * i.Idf);
            features.TotalInformationOfMatchedPart = features.CandidateTermSet.Sum(i => Math.Min(i.NameTerm.Tf, 5) * i.NameTerm.Idf);
            features.MutualInformation= features.CandidateTermSet.Sum(i => Math.Min(i.NameTerm.Tf, 5) * i.NameTerm.Idf * i.Similarity);
            features.EditDistance = features.CandidateTermSet.Sum(i => i.EditDistance);
            features.MaxDistance = features.CandidateTermSet.Sum(i => i.MaxEditDistance);
            features.NameTermCount = allNameTerms.Count;
            

            allNameTerms.ForEach(i=>features.LengthOfTotalRecord += i.Term.Length);

            features.CandidateTermSet.Select(i=>i.NameTerm).ToList().ForEach(i => features.LengthOfTotalMatch += i.Term.Length);
            features.CandidateTermSet.Where(i=>i.Similarity == 1 ).Select(i => i.NameTerm).ToList().ForEach(i => features.LengthOfTotalExactMatch += i.Term.Length);

            
            features.SDNItem = SQLDB.GetSDN(uid);
            
            return features;
        }

        private CandidateTermSet GetMaximalMatches(CandidateTermSet termset)
        {
            if(termset.Count>=1 && termset.Count == termset.NameTerms.Count && termset.Count == termset.QueryTerms.Count)
                return termset;

            termset.NameTerms.Values.ToList().ForEach(i=>i.Index = 0);
            termset.QueryTerms.Values.ToList().ForEach(i => i.Index = 0);
            int queryInd = 0;
            termset.QueryTerms.Values.ToList().ForEach(i => i.Index = queryInd++);

            int recordInd = 0;
            termset.NameTerms.Values.ToList().ForEach(i => i.Index = recordInd++);


            CandidateTermSet maximalSignificantMatches = new CandidateTermSet();
            
            if (termset.QueryTerms.Count <= termset.NameTerms.Count)
            {
                double[,] costMatrix = new double[termset.QueryTerms.Count, termset.NameTerms.Count];

                for (int i = 0; i < termset.QueryTerms.Count; i++)
                {
                    for (int j = 0; j < termset.NameTerms.Count; j++)
                    {
                        costMatrix[i, j] = 1000;
                    }
                }

                termset.ToList().ForEach(i => costMatrix[i.QueryTerm.Index, i.NameTerm.Index] = i.EditDistance);

                int[] matches = HungarianAlgorithm.FindAssignments(costMatrix);

                for (int queryIndex = 0; queryIndex < matches.Length; queryIndex++)
                {
                    int recordIndex = matches[queryIndex];

                    var selectedMatch = (from sm in termset
                                         where
                                         sm.NameTerm.Index == recordIndex &&
                                         sm.QueryTerm.Index == queryIndex
                                         select sm).FirstOrDefault();
                    if (selectedMatch != null)
                        maximalSignificantMatches.Add(selectedMatch);
                }
            }
            else
            {
                double[,] costMatrix = new double[termset.NameTerms.Count, termset.QueryTerms.Count];

                for (int i = 0; i < termset.NameTerms.Count; i++)
                {
                    for (int j = 0; j < termset.QueryTerms.Count; j++)
                    {
                        costMatrix[i, j] = int.MaxValue;
                    }
                }

                termset.ToList().ForEach(i => costMatrix[i.NameTerm.Index, i.QueryTerm.Index] = i.EditDistance);

                int[] matches = HungarianAlgorithm.FindAssignments(costMatrix);

                for (int recordIndex = 0; recordIndex < matches.Length; recordIndex++)
                {
                    int queryIndex = matches[recordIndex];

                    CandidateMatchTerm selectedMatch = (from sm in termset
                                                where
                                                sm.NameTerm.Index == recordIndex &&
                                                sm.QueryTerm.Index == queryIndex
                                                select sm).FirstOrDefault();

                    if (selectedMatch != null)
                        maximalSignificantMatches.Add(selectedMatch);
                }
            }

            return maximalSignificantMatches;
        }

        private double CalculateMutualInformation(CandidateTermSet termset)
        {
            var sum = 0.0;
            foreach (var matchTerm in termset)
            {
                sum += matchTerm.NameTerm.Idf;
            }

            return sum;
        }
    }
}
