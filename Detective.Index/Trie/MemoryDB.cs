using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Index.Trie
{
    public class MemoryDB
    {
        private static int TrieNodeIdCounter;

        public static TrieNode CreateTrieNode(char ch)
        {
            TrieNode trieNode = new TrieNode();
            trieNode.Char = ch;
            trieNode.Id = TrieNodeIdCounter++;

            return trieNode;
        }

        private static int NameTermIdCounter;

        public static Dictionary<string, NameTerm> NameTermsByString = new Dictionary<string, NameTerm>();
        public static Dictionary<int, NameTerm> NameTerms = new Dictionary<int, NameTerm>();
        public static Dictionary<int, Dictionary<int, List<NameTerm>>> AllNameTerms = new Dictionary<int, Dictionary<int, List<NameTerm>>>();

        public static NameTerm CreateNameTerm(string term)
        {
            if (NameTermsByString.ContainsKey(term))
                return NameTermsByString[term];

            NameTerm nameTerm = new NameTerm() { Term = term };
            nameTerm.Id = NameTermIdCounter++;

            NameTerms[nameTerm.Id] = nameTerm;
            NameTermsByString[term] = nameTerm;
            return nameTerm;
        }

        public static void AddNameTerm(int uid, int nameIndex, NameTerm nameTerm)
        {
            if(!AllNameTerms.ContainsKey(uid))
                AllNameTerms[uid] = new Dictionary<int, List<NameTerm>>();

            if(!AllNameTerms[uid].ContainsKey(nameIndex))
                AllNameTerms[uid][nameIndex] = new List<NameTerm>();

            AllNameTerms[uid][nameIndex].Add(nameTerm);
        }

        public static List<NameTerm> GetAllNameTerms(int uid, int nameIndex)
        {
            return AllNameTerms[uid][nameIndex];
        }

        private static TrieNode root;

        public static TrieNode RootNode { get
            {
                if (root == null)
                    root = MemoryDB.CreateTrieNode('\0');

                return root;
            }
        }
        
    }
}
