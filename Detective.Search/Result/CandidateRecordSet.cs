using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Result
{
    public class CandidateRecordSet: Dictionary<int, CandidateName>
    {
        public void Merge(CandidateRecordSet candidateRecordSet)
        {
            foreach (var item in candidateRecordSet)
            {
                if (this.ContainsKey(item.Key))
                    this[item.Key].Merge(item.Value);
                else
                    this[item.Key] = item.Value;
            }
        }

        public new void Add(int key, CandidateName candidateName)
        {
            if (!this.ContainsKey(key))
                this[key] = candidateName;

            this[key].Merge(candidateName);
        }

        public void Add(int id, int nameIndex, CandidateMatchTerm candidateTerm)
        {
            var candidateTermSet = new CandidateTermSet();
            candidateTermSet.Add(candidateTerm);

            var candidateName = new CandidateName();
            candidateName[nameIndex] = candidateTermSet;

            this.Add(id, candidateName);
        }
    }
}
