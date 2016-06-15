using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Index.Trie;

namespace Detective.Search.Filter
{
    public class PostingListTraverser
    {
        public List<NameTermPosting> Intersect(List<NameTerm> nameTerms)
        {
            List<NameTerm> inputTerms = new List<NameTerm>(nameTerms);

            var terms = inputTerms.OrderBy(i => i.PostingList.Count);
            var result = terms.First().PostingList;
            var restTerms = terms.Skip(1).ToList();

            while (restTerms.Any() && result.Count > 0)
            {
                var firstTermPosting = restTerms.First().PostingList;
                result = Intersect(result, firstTermPosting);
                restTerms = restTerms.Skip(1).ToList();
            }

            return result;
        }

        public List<NameTermPosting> Intersect(List<NameTermPosting> p1, List<NameTermPosting> p2)
        {
            List<NameTermPosting> answer = new List<NameTermPosting>();

            int i = 0, j = 0;
            while (i < p1.Count && j < p2.Count)
            {
                int comparison = Compare(p1[i], p2[j]);
                if (comparison == 0)
                {
                    answer.Add(p1[i]);
                    i++;
                    j++;
                }
                else if (comparison < 0)
                    i++;
                else
                    j++;
            }
            return answer;
        }

        public int Compare(NameTermPosting d1, NameTermPosting d2)
        {
            if (d1.Uid < d2.Uid)
                return -1;

            if (d1.Uid > d2.Uid)
                return 1;

            if (d1.NameIndex < d2.NameIndex)
                return -1;

            if (d1.NameIndex > d2.NameIndex)
                return 1;

            return 0;
        }
    }
}
