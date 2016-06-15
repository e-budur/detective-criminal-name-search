using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Result
{
    public class CandidateName : Dictionary<int, CandidateTermSet>
    {
        public void Merge(CandidateName candidateName)
        {
            foreach (var pair in candidateName)
            {
                if(this.ContainsKey(pair.Key))
                    this[pair.Key].Merge(pair.Value);
                else
                    this[pair.Key] = pair.Value;
            }
        }

        public void Add(int key, CandidateMatchTerm candidateTerm)
        {
            if(this.ContainsKey(key))
            {
                this[key].Add(candidateTerm);
            }else
            {
                var candidateTermSet = new CandidateTermSet();
                candidateTermSet.Add(candidateTerm);
                this[key] = candidateTermSet;
            }

        }
        
        
            
    }
}
