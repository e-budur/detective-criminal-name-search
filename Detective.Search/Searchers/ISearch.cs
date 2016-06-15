using Detective.Query.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Search
{
    public interface ISearch
    {
        CandidateRecordSet Search(string query);
    }
}
