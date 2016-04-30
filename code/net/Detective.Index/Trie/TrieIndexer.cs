using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Commons.Lang;
using Detective.Data;

namespace Detective.Index.Trie
{
    public class TrieIndexer : IIndexer
    {
        private Dictionary<int, int> NameIndexes = new Dictionary<int, int>();

        public void Index(int uid, string name)
        {
            Console.WriteLine("I indexed "+ uid + " - " + name);
            Ingest(uid, name);
        }

        private void Ingest(int uid, string name)
        {
            var normalizedName = name.NormalizeString();
            var nameIndex = IncrementNameIndex(uid);
            SQLDB.AddName(uid, nameIndex, name);
            var normalizedTerms = normalizedName.SplitString();
            var sequenceNum = 0;
            foreach (var normalizedTerm in normalizedTerms)
            {
                var nameTerm = MemoryDB.CreateNameTerm(normalizedTerm);
                Ingest(nameTerm);
                nameTerm.AddPosting(uid, nameIndex, sequenceNum);
                sequenceNum++;
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
