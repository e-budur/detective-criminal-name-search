using Detective.Index;
using Detective.Index.Trie;
using Detective.Query;
using Detective.Search;
using Detective.Search.Trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Api;
using Detective.Query.Searchers;
using Detective.Search.Algorithms;

namespace Detective.TestBed
{
    public class TrieSearcherTest
    {
        public static void Main(string[] args)
        {
            EditCostMatrix.Build();
            TrieIndexer.Index();
            
            Finder finder = new Finder();

            while (true)
            {
                int i = 0;

                Console.Write("Query:");
                var query = Console.ReadLine();
                var candidateSearchResult = finder.Query(query);
                foreach (var searchItem in candidateSearchResult.SearchItems)
                {
                    Console.WriteLine(searchItem.Uid+ "-"+ searchItem.Score+"  "+ searchItem.Fullname);
                    i++;
                    if (i > 30)
                        break;
                }

                Console.WriteLine();
            }



        }
    }
}
