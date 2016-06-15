using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Index.Trie;
using Detective.Query;
using Detective.Search.Algorithms;

namespace Detective.TestBed
{
    public class DetailsTest
    {
        public static void Main(string[] args)
        {
            EditCostMatrix.Build();
            TrieIndexer.Index();

            var finder = new Finder();
            while (true)
            {
                int i = 0;

                Console.Write("Uid:");
                var uidString = Console.ReadLine();
                int uid = int.Parse(uidString);
                var candidateSearchResult = finder.GetDetails(uid);
                foreach (var searchItem in candidateSearchResult.SearchItems)
                {
                    Console.WriteLine(searchItem.Uid + "-" + searchItem.Score + "  " + searchItem.Fullname);
                    i++;
                    if (i > 10)
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
