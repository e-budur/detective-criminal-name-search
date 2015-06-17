using Detective.Commons.Lang;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Query
{
    public class QueryParser
    {
        private string query;
        public QueryParser(string query)
        {
            this.query = query;
        }

        public QueryTerm[] ParseTerms()
        {
            return this.query.SplitString().Select(i => new QueryTerm() { Term = i }).ToArray();
        }
    }
}
