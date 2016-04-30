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

namespace Detective.TestBed
{
    public class TrieSearcherTest
    {
        public static void Main(string[] args)
        {
            IIndexer indexer = new TrieIndexer();
            
            string[] lines = System.IO.File.ReadAllLines(@"Data\sdn_parsed_data.txt");

            int count = 100;
            foreach (var line in lines)
            {
                var cols = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                int uid = int.Parse(cols[0]);
                string name = cols[1];
                indexer.Index(uid, name);
                count--;
                if (count == 0)
                    break;
            }
            
            Finder finder = new Finder();

            while (true)
            {
                Console.Write("Query:");
                var query = Console.ReadLine();
                var candidateSearchResult = finder.Query(query);
                foreach (var searchItem in candidateSearchResult.SearchItems)
                {
                    Console.WriteLine(searchItem.Uid+ "-"+ searchItem.NameIndex+"  "+ searchItem.Fullname);
                }

                Console.WriteLine();
            }



        }
    }
}
