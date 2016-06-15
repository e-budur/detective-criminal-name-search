using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Index.Trie;
using Detective.Query.Result;
using Detective.Search.Utils;

namespace Detective.Search.Filter
{
    public class FrequentItemSetCalculator
    {
        private Dictionary<List<NameTerm>, int> frequencyCounts = new Dictionary<List<NameTerm>, int>(new ListComparer<NameTerm>());
        private int supportThreashold;

        public FrequentItemSetCalculator(int supportThreashold)
        {
            this.supportThreashold = supportThreashold;
        }

        public void SetFrequentItemSets(CandidateRecordSet worldCheckMatches)
        {
            PostingListTraverser postingListTraverser = new PostingListTraverser();
            foreach (var worldCheckMatch in worldCheckMatches)
            {
                foreach (var nameIndexMatch in worldCheckMatch.Value)
                {
                    var match = nameIndexMatch.Value;
                    var nameTerms = match.NameTerms.Values.ToList();

                    int docFrequency;

                    if (frequencyCounts.ContainsKey(nameTerms))
                        docFrequency = frequencyCounts[nameTerms];
                    else
                    {
                        var intersectionList = postingListTraverser.Intersect(nameTerms);
                        docFrequency = intersectionList.Select(i => i.Uid).Distinct().Count();
                        frequencyCounts[nameTerms] = docFrequency;
                    }
                    match.Support = docFrequency;

                    
                    bool tmpIsFrequent = (docFrequency >= this.supportThreashold);

                    if (nameTerms.Count == 1 && docFrequency >= 5)
                        tmpIsFrequent = true;

                    if (tmpIsFrequent)
                        match.IsFrequent = tmpIsFrequent;
                }
            }
        }
    }
}
