using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Query.Result;
using Detective.Search.Rank;

namespace Detective.Query.Rank
{
    class TfIdfScorer:IScore
    {
        public ScoredMatch Score(MatchFeatures features)
        {
            var scoredMatch = new ScoredMatch();

            scoredMatch.Score = (int)(100*features.TfIdfSimilarity);
            scoredMatch.Id = features.Uid;
            scoredMatch.NameIndex = features.NameIndex;

            return scoredMatch;
        }
    }
}
