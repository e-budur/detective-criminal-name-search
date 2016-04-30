using Detective.Index.Trie;
using Detective.Query.Query;
using Detective.Query.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Searchers
{
    public struct TrieSearchParams
    {
        public TrieNode CurrentNode;

        public CandidateRecordSet CandidateRecordSet;
        public double[] CurrentRow;
        public double[] PreviousRow;
        public char[] QueryChars;
        public int IndexOfName;
        private QueryTerm queryTerm;
        public double MaxAllowedEditCost;

        public double SimilarityDistance
        {
            get { return this.CurrentRow[this.CurrentRow.Length - 1]; }
        }
        public QueryTerm QueryTerm
        {
            get
            {
                return queryTerm;
            }
            set
            {
                queryTerm = value;
            }
        }
        public int QueryLength { get { return this.queryTerm.Term.Length; } }

        public TrieSearchParams(QueryTerm queryTerm)
        {
            this.CurrentNode = null;
            this.queryTerm = queryTerm;
            QueryChars = queryTerm.Term.ToLowerInvariant().ToArray();

            this.CurrentRow = new double[queryTerm.Term.Length+1];
            for (int i = 0; i < this.CurrentRow.Length; i++)
                this.CurrentRow[i] = i;

            this.PreviousRow = null;

            this.IndexOfName = 0;

            this.CandidateRecordSet = new CandidateRecordSet();

            this.MaxAllowedEditCost = 1;
        }

        internal void InitNextRowSetting()
        {
            this.PreviousRow = CurrentRow;
            this.CurrentRow = new double[this.PreviousRow.Length];
            this.CurrentRow[0] = this.PreviousRow[0] + 1;
        }

    }
}
