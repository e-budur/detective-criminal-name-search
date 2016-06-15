using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Commons.Lang;
using Detective.Data;
using Detective.DataModel;
using Detective.Feeder;
using NLog;

namespace Detective.Index.Trie
{
    public class TrieIndexer : IIndexer
    {
        private bool indexReady;
        private static object indexReadyLock = new object();

        private Dictionary<int, int> NameIndexes = new Dictionary<int, int>();

        
        public static void Index()
        {
            Logger logger = LogManager.GetLogger("logfile");

            logger.Info("Indexing...");
            var indexer = new TrieIndexer();
            SDNFileReader sdnFileReader = new SDNFileReader("https://www.treasury.gov/ofac/downloads/sdn.xml");
            int i = 0;
            SQLDB.ClearAllSDNs();
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

                SQLDB.AddSDN(item);

            }

            AddSDNRecord(indexer, 100001, "Hong Kong Dong");
            AddSDNRecord(indexer, 100002, "Suriye");
            AddSDNRecord(indexer, 100003, "Global Securities Corporation");
            AddSDNRecord(indexer, 100004, "International Technologies Corporation");
            AddSDNRecord(indexer, 100005, "INTERNATIONAL BANK");

            Console.WriteLine("SDNCount : " + i);
            indexer.Complete();
        }

        public static void AddSDNRecord(IIndexer indexer, int uid, string name)
        {
            indexer.Index(uid, name);
            SQLDB.AddSDN(new SDNItem() { first_name = "", last_name = name, uid = uid, sdn_type = "Entity",remarks = "Sythetically inserted for demo" });
        }

        public void Index(int uid, string name)
        {
            Ingest(uid, name);
        }

        public void Complete()
        {
            CalculateIdfs();
        }

        private void CalculateIdfs()
        {
            var N = SQLDB.GetDocCount()*1.0;
            foreach (var nameTerm in MemoryDB.NameTerms.Values)
                nameTerm.Idf = Math.Log(N/nameTerm.DocFrequency);
            
        }

        private void Ingest(int uid, string name)
        {
            var normalizedName = name.NormalizeString();
            var nameIndex = IncrementNameIndex(uid);
            
            var normalizedTerms = normalizedName.SplitString();
            var sequenceNum = 0;
            foreach (var normalizedTerm in normalizedTerms)
            {
                var nameTerm = MemoryDB.CreateNameTerm(normalizedTerm);
                MemoryDB.AddNameTerm(uid, nameIndex, nameTerm);
                Ingest(nameTerm);
                nameTerm.AddPosting(uid, nameIndex, sequenceNum++);
            }
            
        } 

        private void Ingest(NameTerm token)
        {
            var term = token.Term;

            var currentNode = MemoryDB.RootNode;
            for (int i = 0; i < term.Length; i++)
            {
                char ch = term[i];
                if (!currentNode.Keys.Contains(ch))
                    currentNode = currentNode.AddChild(ch);
                else
                    currentNode = currentNode[ch];
            }
            currentNode.AddTermId(token.Id);
        }

        public int IncrementNameIndex(int uid)
        {
            if (!this.NameIndexes.Keys.Contains(uid))
                this.NameIndexes[uid] = 0;
            else
                this.NameIndexes[uid]++;

            return this.NameIndexes[uid];
        }
    }
}
