using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Index.Trie
{
    public class TrieNode : Dictionary<char, TrieNode>
    {
        public int Id { get; set; }
        public char Char { get; set; }
        public bool EOW { get; private set; }

        public HashSet<int> TermIds = new HashSet<int>();

        private void AddChild(TrieNode child)
        {
            this[child.Char] = child;
        }

        public TrieNode AddChild(char ch)
        {
            if (this.Keys.Contains(ch))
                return this[ch];

            var child = MemoryDB.CreateTrieNode(ch);
            this.AddChild(child);
            return child;
        }

        public void AddTermId(int termId)
        {
            if (this.TermIds.Contains(termId))
                return;

            this.TermIds.Add(termId);

            this.EOW = true;
        }
        
    }
}
