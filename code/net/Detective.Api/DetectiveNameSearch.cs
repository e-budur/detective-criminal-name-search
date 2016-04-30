using Detective.DataModel;
using Detective.Index.Trie;
using Detective.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Detective.Api
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class DetectiveNameSearch : INameSearch
    {
        public DetectiveNameSearch()
        {
            Index();
        }
        public void Index()
        {
            string[] lines = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory+@"Data\sdn_parsed_data.txt");

            var indexer = new TrieIndexer();
            
            foreach (var line in lines)
            {
                var cols = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                int uid = int.Parse(cols[0]);
                string name = cols[1];
                indexer.Index(uid, name);
            }
        }

        public QueryResult Query(QueryRequest query)
        {
            throw new NotImplementedException();
        }

        public QueryResult Query(string query)
        {
            Finder finder = new Finder();

            var candidateSearchResult = finder.Query(query);
            foreach (var searchItem in candidateSearchResult.SearchItems)
            {
                Console.WriteLine(searchItem.Uid + "-" + searchItem.NameIndex + "  " + searchItem.Fullname);
            }
            
            return candidateSearchResult;
        }
    }
}
