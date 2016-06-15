using Detective.Index.Trie;
using Detective.Query.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Result
{
    public class CandidateMatchTerm
    {
        public QueryTerm QueryTerm;
        public NameTerm NameTerm;
        public double Uid;
        public double NameIndex;
        public double EditDistance;
        public int MaxEditDistance;

        public double Similarity
        {
            get { return 1-EditDistance*1.0/MaxEditDistance; }
        }

        public CandidateMatchTerm(QueryTerm queryTerm, NameTerm nameTerm, double editDistance)
        {
            this.QueryTerm = queryTerm;
            this.NameTerm = nameTerm;

            this.EditDistance = editDistance;
            this.MaxEditDistance = Math.Max(this.QueryTerm.Term.Length, this.NameTerm.Term.Length);
        }

        public override bool Equals(object obj)
        {
            CandidateMatchTerm otherMatch = obj as CandidateMatchTerm;

            if (otherMatch == null)
                return false;

            return this.QueryTerm.Equals(otherMatch.QueryTerm) && this.NameTerm.Equals(otherMatch.NameTerm);
        }
    }
}
