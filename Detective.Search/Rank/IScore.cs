using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Query.Result;

namespace Detective.Search.Rank
{
    public interface IScore
    {
        ScoredMatch Score(MatchFeatures features);
    }
}
