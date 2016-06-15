using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Query.Rank;
using Detective.Query.Result;

namespace Detective.Search.Rank
{
    public class DefaultRanker
    {

        public ScoredMatch[] Rank(CandidateRecordSet candidateRecordSet)
        {
            if(candidateRecordSet.Count == 0)
                return new ScoredMatch[0];

            var scoredMatchList = new List<ScoredMatch>();

            foreach (var candidateRecord in candidateRecordSet)
            {
                var uid = candidateRecord.Key;
                var record = candidateRecord.Value;

                var scoredMatch = Rank(uid, record);

                if(scoredMatch != null && scoredMatch.Score > 0)
                    scoredMatchList.Add(scoredMatch);
            }

            var scoredMatches = scoredMatchList.OrderByDescending(i => i.Score).ToArray();

            return scoredMatches;
        }

        public ScoredMatch Rank(int uid, CandidateName record)
        {
            var candidateMatches = new List<ScoredMatch>();

            var featurizer = new MatchFeaturizer();
            foreach (var recordName in record)
            {
                var nameIndex = recordName.Key;
                var candidateTermSet = recordName.Value;
                var features = featurizer.Featurize(uid, nameIndex, candidateTermSet);
                var scorer = ScorerFactory.CreateScorer(features);

                var scoredMatch = scorer.Score(features);

                if(scoredMatch != null)
                    candidateMatches.Add(scoredMatch);
            }

            var topMatch = candidateMatches.OrderByDescending(i => i.Score).FirstOrDefault();

            return topMatch;
        }
        
    }
}
