using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Search.Rank
{
    public class IndividualScorer: IScore
    {
        public ScoredMatch Score(MatchFeatures features)
        {
            var scoredMatch = new ScoredMatch();
            var mutualInformationWeight = features.TfIdfSimilarity;
            if (features.LengthOfTotalExactMatch > 7 &&
                features.WeightOfLengthOfTotalMatch > features.MutualInformationWeight)
            {
                mutualInformationWeight = features.WeightOfLengthOfTotalMatch;
            }

            if (features.MatchedNameTermCount == 1 && features.NameTermCount > features.MatchedNameTermCount &&  features.CandidateTermSet.Support > 3)
                mutualInformationWeight = 0;

            scoredMatch.Score = (int)(100 * mutualInformationWeight);
            scoredMatch.Id = features.Uid;
            scoredMatch.NameIndex = features.NameIndex;

            return scoredMatch;
        }
    }
}
