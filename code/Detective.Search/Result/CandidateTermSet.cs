using Detective.Index.Trie;
using Detective.Query.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Result
{
    public class CandidateTermSet : Collection<CandidateMatchTerm>
    {
        private Dictionary<string, QueryTerm> QueryTerms = new Dictionary<string, QueryTerm>();
        private Dictionary<int, NameTerm> NameTerms = new Dictionary<int, NameTerm>();

        public void Merge(CandidateTermSet candidateTermSet)
        {
            foreach (var candidateTerm in candidateTermSet)
            {
                this.Add(candidateTerm);
            }
        }

        public new void Add(CandidateMatchTerm candidateTerm)
        {
            if (this.Contains(candidateTerm))
                return;

            AddQueryTerm(candidateTerm.QueryTerm);
            AddNameTerm(candidateTerm.NameTerm);

            candidateTerm.QueryTerm = this.QueryTerms[candidateTerm.QueryTerm.Term];
            candidateTerm.NameTerm = this.NameTerms[candidateTerm.NameTerm.Id];
            
            base.Add(candidateTerm);
        }
        
        private void AddQueryTerm(QueryTerm queryTerm)
        {
            if (this.QueryTerms.ContainsKey(queryTerm.Term))
                return;

            this.QueryTerms[queryTerm.Term] = queryTerm;
        }

        private void AddNameTerm(NameTerm nameTerm)
        {
            if (this.NameTerms.ContainsKey(nameTerm.Id))
                return;

            this.NameTerms[nameTerm.Id] = nameTerm;
        }

    }
}
