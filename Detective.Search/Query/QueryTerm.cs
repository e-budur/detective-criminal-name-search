using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Query
{
    public class QueryTerm
    {
        public int Index { get; set; }

        public string Term;

        public bool Equals(QueryTerm otherTerm)
        {
            if (otherTerm == null)
                return false;

            return this.Term.CompareTo(otherTerm.Term) == 0;
        }
    }
}
