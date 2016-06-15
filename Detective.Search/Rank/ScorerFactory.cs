using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Query.Rank;

namespace Detective.Search.Rank
{
    public class ScorerFactory
    {

        public static IScore CreateScorer(MatchFeatures features)
        {
            switch (features.SDNType)
            {
                case "Individual":
                    return new IndividualScorer();
                    break;
                default:
                    return new TfIdfScorer();
            }
        }
    }
}
