using Detective.Index;
using Detective.Index.Trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.TestBed
{
    public class IndexBuilderTest
    {
        public static void Main(string[] args)
        {
            IIndexer indexer = new TrieIndexer();

            string[] lines = System.IO.File.ReadAllLines(@"Data\sdn_parsed_data.txt");

            foreach (var line in lines)
            {
                var cols = line.Split("|".ToCharArray());
                int uid = int.Parse(cols[0]);
                string name = cols[1];
                indexer.Index(uid, name);
            }


        }
    }
}
