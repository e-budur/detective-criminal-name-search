using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Query.Result;

namespace Detective.Search.Filter
{
    public class FrequentMatchFilter
    {
        public CandidateRecordSet Filter(CandidateRecordSet inputRecordSet)
        {
            var NoisyWordMatchFrequencyCutoffThreshold = 8;
            FrequentItemSetCalculator filteredItemSetCalculator = new FrequentItemSetCalculator(NoisyWordMatchFrequencyCutoffThreshold);
            filteredItemSetCalculator.SetFrequentItemSets(inputRecordSet);

            CandidateRecordSet filteredMatches = new CandidateRecordSet();

            foreach (var worldCheckEntry in inputRecordSet)
            {
                CandidateName filteredNameMatchesDictionary = new CandidateName();

                foreach (KeyValuePair<int, CandidateTermSet> nameMatchEntry in worldCheckEntry.Value)
                {
                    var tokenMatchCollection = nameMatchEntry.Value;
                    if (tokenMatchCollection.IsFrequent)
                    {
                        Console.WriteLine();
                        continue;
                    }
                    filteredNameMatchesDictionary[nameMatchEntry.Key] = tokenMatchCollection;
                }

                if (filteredNameMatchesDictionary.Count > 0)
                    filteredMatches[worldCheckEntry.Key] = filteredNameMatchesDictionary;
            }

            return filteredMatches;
        }
    }
}
