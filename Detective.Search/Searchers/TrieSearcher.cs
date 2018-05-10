using Detective.Index.Trie;
using Detective.Query.Query;
using Detective.Query.Result;
using Detective.Query.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Search.Algorithms;
using Detective.Search.Utils;

namespace Detective.Search.Trie
{

    public class TrieSearcher:ISearch
    {
        
        public CandidateRecordSet Search(string query)
        {
            var queryParser = new QueryParser(query);
            var queryTerms = queryParser.ParseTerms();
            var candidateRecordSet = new CandidateRecordSet();
            foreach (var queryTerm in queryTerms)
            {
                var tmpCandidateRecordSet = Search(queryTerm);
                candidateRecordSet.Merge(tmpCandidateRecordSet);
            }
            return candidateRecordSet;
        }

        private CandidateRecordSet Search(QueryTerm queryTerm)
        {
            TrieSearchParams searchParams = new TrieSearchParams(queryTerm);
            
            foreach (var childChar in MemoryDB.RootNode.Keys)
            {
                searchParams.CurrentNode = MemoryDB.RootNode[childChar];

                Search(searchParams);
            }

            return searchParams.CandidateRecordSet;
        }

        private void Search(TrieSearchParams searchParams)
        {
            searchParams.InitNextRowSetting();

            EditCost currentEditCost = new EditCost();
            currentEditCost.CurrentNameChar = searchParams.CurrentNode.Char;
            var minimumEditCost = searchParams.CurrentRow[0];
            for (int i = 1; i < searchParams.PreviousRow.Length; i++)
            {
                currentEditCost.CurrentQueryChar = searchParams.QueryChars[i - 1];

                var insertionUnitCost = EditCostMatrix.getInsertionProb(currentEditCost.PreviousNameChar, currentEditCost.CurrentNameChar);
                var deletionUnitCost = EditCostMatrix.getDeletionProb(currentEditCost.PreviousQueryChar, currentEditCost.CurrentQueryChar);
                var substituionUnitCost = EditCostMatrix.getSubstitutionProb(currentEditCost.CurrentQueryChar, currentEditCost.CurrentNameChar);

                currentEditCost.InsertCost = searchParams.CurrentRow[i - 1] + insertionUnitCost;
                currentEditCost.DeletionCost = searchParams.PreviousRow[i] + deletionUnitCost;
                currentEditCost.SubstituionCost = searchParams.PreviousRow[i-1] + substituionUnitCost;
                searchParams.CurrentRow[i] = currentEditCost.MinCost;
                minimumEditCost = currentEditCost.MinCost < minimumEditCost ? currentEditCost.MinCost : minimumEditCost;
            }

            if(searchParams.SimilarityDistance <= searchParams.MaxAllowedEditCost && searchParams.CurrentNode.EOW)
                CollectCandidateMatches(searchParams);

            if(minimumEditCost <= searchParams.MaxAllowedEditCost)
            {
                var currentNode = searchParams.CurrentNode;
                searchParams.CurrentNameLength++;
                searchParams.SetMaxEditCost((int) Math.Max(searchParams.CurrentNameLength, searchParams.QueryLength));
                foreach (var childChar in currentNode.Keys)
                {
                    searchParams.CurrentNode = currentNode[childChar];
                    Search(searchParams);
                }
            }
        }

        
        private void CollectCandidateMatches(TrieSearchParams searchParams)
        {
            foreach (var candidateTermId in searchParams.CurrentNode.TermIds)
            {
                var candidateNameTerm = MemoryDB.NameTerms[candidateTermId];
                var candidateTerm = new CandidateMatchTerm(searchParams.QueryTerm, candidateNameTerm, searchParams.SimilarityDistance);
                foreach (var posting in candidateNameTerm.PostingList)
                {
                    candidateTerm.Uid = posting.Uid;
                    candidateTerm.NameIndex = posting.NameIndex;
                    searchParams.CandidateRecordSet.Add(posting.Uid, posting.NameIndex, candidateTerm);
                }
            }
        }
    }
}
