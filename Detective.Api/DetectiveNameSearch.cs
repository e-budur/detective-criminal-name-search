using Detective.Data;
using Detective.Feeder;
using Detective.Index.Trie;
using Detective.Query;
using System;
using System.Linq;
using System.Diagnostics;
using EntityFramework.BulkInsert.Extensions;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using Detective.Commons.Utils;
using Detective.DataModel;
using Detective.Index;

namespace Detective.Api
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class DetectiveNameSearch : INameSearch
    {

        public DetectiveNameSearch()
        {
            
        }

        
        public void Index()
        {
            var indexer = new TrieIndexer();
            SDNFileReader sdnFileReader = new SDNFileReader("https://www.treasury.gov/ofac/downloads/sdn.xml");
            int i = 0;
            foreach (var item in sdnFileReader.ReadSDNItem())
            {
                var uid = item.uid;
                var fullname = item.first_name + " " + item.last_name;
                i++;
                indexer.Index(uid, fullname);

                foreach (var aka in item.AkaList)
                {
                    fullname = aka.first_name + " " + aka.last_name;
                    Console.WriteLine(string.Format("{0}-{1} {2}", uid, aka.first_name, aka.last_name));
                    indexer.Index(uid, fullname);
                }

            }
            
            Console.WriteLine("SDNCount : " + i);
            indexer.Complete();
        }

        

        public QueryResult Query(QueryRequest query)
        {
            throw new NotImplementedException();
        }

        public QueryResult Query(string query)
        {
            Logger.WriteInfo("Query:" + query);
            Finder finder = new Finder();

            var candidateSearchResult = finder.Query(query);

            Logger.WriteInfo("SearchTime:" + candidateSearchResult.SearchTime);

            candidateSearchResult.Query = query;

            return candidateSearchResult;
        }

        public QueryResult GetDetails(string uidStr)
        {
            Finder finder = new Finder();

            int uid = int.Parse(uidStr);
            var candidateSearchResult = finder.GetDetails(uid);

            return candidateSearchResult;
        }
        
    }
}
