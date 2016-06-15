using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Detective.Search.Algorithms
{
    public class ConfusionMatrix
    {
        public const string INSERTION_CONFIG_KEY = "confusion_insertion_matrix";
        public const string DELETION_CONFIG_KEY = "confusion_deletion_matrix";
        public const string SUBSTITUTION_CONFIG_KEY = "confusion_substitution_matrix";
        public const string TRANSPOSITION_CONFIG_KEY = "confusion_transposition_matrix";
        public const string BIGRAM_CONFIG_KEY = "confusion_bigram_matrix";
        public const string UNIGRAM_CONFIG_KEY = "confusion_unigram_counts";
        
        public ConfusionMatrix()
        {

        }
        private Dictionary<char, int> unigramCounts = new Dictionary<char, int>();
        private SimpleMatrix bigramCounts = new SimpleMatrix();
        private SimpleMatrix deletionCounts = new SimpleMatrix();
        private SimpleMatrix insertionCounts = new SimpleMatrix();
        private SimpleMatrix substitutionCounts = new SimpleMatrix();
        private SimpleMatrix transpositionCounts = new SimpleMatrix();

        public SimpleMatrix GetBigramMatrix()
        {
            return bigramCounts;
        }

        public SimpleMatrix GetDeletionMatrix()
        {
            return deletionCounts;
        }


        public SimpleMatrix GetInsertionMatrix()
        {
            return insertionCounts;
        }

        public SimpleMatrix GetSubstitutionMatrix()
        {
            return substitutionCounts;
        }

        public SimpleMatrix GetTranspositionMatrix()
        {
            return transpositionCounts;
        }

        public Dictionary<char, int> GetUnigramCounts()
        {
            return unigramCounts;
        }

        public void ReduceDeletion(char ch1, char ch2)
        {
            deletionCounts.substract(ch1, ch2, 1);
        }

        public void ReduceInsertion(char ch1, char ch2)
        {
            insertionCounts.substract(ch1, ch2, 1);
        }

        public void ReduceSubstitution(char ch1, char ch2)
        {
            substitutionCounts.substract(ch1, ch2, 1);
        }

        public void ReduceTransposition(char ch1, char ch2)
        {
            transpositionCounts.substract(ch1, ch2, 1);
        }

        public void AddDeletion(char ch1, char ch2)
        {
            AddUnigrams(ch1);
            AddUnigrams(ch2);
            deletionCounts.add(ch1, ch2, 1);
        }

        public void AddInsertion(char ch1, char ch2)
        {
            AddUnigrams(ch1);
            AddUnigrams(ch2);
            insertionCounts.add(ch1, ch2, 1);
        }

        public void AddSubstitution(char ch1, char ch2)
        {
            AddUnigrams(ch1);
            AddUnigrams(ch2);
            substitutionCounts.add(ch1, ch2, 1);
        }

        public void AddTransposition(char ch1, char ch2)
        {
            AddUnigrams(ch1);
            AddUnigrams(ch2);
            transpositionCounts.add(ch1, ch2, 1);
        }

        public void SetDeletion(char ch1, char ch2, int freq)
        {
            deletionCounts.set(ch1, ch2, freq);
        }

        public void SetInsertion(char ch1, char ch2, int freq)
        {
            insertionCounts.set(ch1, ch2, freq);
        }

        public void SetSubstitution(char ch1, char ch2, int freq)
        {
            substitutionCounts.set(ch1, ch2, freq);
        }

        public void SetTransposition(char ch1, char ch2, int freq)
        {
            transpositionCounts.set(ch1, ch2, freq);
        }

        public void SetBigram(char ch1, char ch2, int freq)
        {
            bigramCounts.set(ch1, ch2, freq);
        }

        public void SetUnigram(char ch, int count)
        {
            unigramCounts[ch] = count;
        }

        public double GetDeletion(char ch1, char ch2)
        {
            return deletionCounts.get(ch1, ch2);
        }

        public double GetInsertion(char ch1, char ch2)
        {
            return insertionCounts.get(ch1, ch2);
        }

        public double GetSubstitution(char ch1, char ch2)
        {
            return substitutionCounts.get(ch1, ch2);
        }

        public double GetTransposition(char ch1, char ch2)
        {
            return transpositionCounts.get(ch1, ch2);
        }

        public double GetUnigramCount(char ch)
        {
            if (unigramCounts.ContainsKey(ch) == false)
                return 0;

            return unigramCounts[ch];
        }

        public double GetBigramCount(char ch1, char ch2)
        {
            return bigramCounts.get(ch1, ch2);
        }

        public int GetDistinctUnigramCount()
        {
            return unigramCounts.Keys.Count;
        }

        public char[] GetDistinctUnigrams()
        {
            return unigramCounts.Keys.ToArray();
        }

        public void AddBigram(char ch1, char ch2)
        {
            this.bigramCounts.add(ch1, ch2, 1);
        }

        public void AddUnigrams(char ch1)
        {
            if (this.unigramCounts.ContainsKey(ch1) == false)
                this.unigramCounts[ch1] = 0;
            
            this.unigramCounts[ch1] = this.unigramCounts[ch1] + 1;
        }

        public void SetBigramMatrix(SimpleMatrix matrix)
        {
            this.bigramCounts = matrix;
        }

        public void SetSubstituionMatrix(SimpleMatrix matrix)
        {
            this.substitutionCounts = matrix;
        }

        public void SetInsertionMatrix(SimpleMatrix matrix)
        {
            this.insertionCounts = matrix;
        }

        public void SetDeletionMatrix(SimpleMatrix matrix)
        {
            this.deletionCounts = matrix;
        }

        public void Dump()
        {
            this.DumpMatrix("substitutionMatrix.txt", this.substitutionCounts);
            this.DumpMatrix("insertionMatrix.txt", this.insertionCounts);
            this.DumpMatrix("deletionMatrix.txt", this.deletionCounts);
            this.DumpMatrix("bigramMatrix.txt", this.bigramCounts);
            this.DumpUnigrams("unigram.txt");
        }

        private void DumpMatrix(string dumpFilename, SimpleMatrix matrix)
        {
            var keys = matrix.getKeys().OrderBy(i=>i);
            string formatStr = "{0,3} ";

            string dumpStr = string.Format(formatStr, " ");

            foreach (var key in keys)
                dumpStr += string.Format(formatStr, key);

            dumpStr += "\r\n";

            foreach (var key1 in keys)
            {
                dumpStr += string.Format(formatStr, key1);

                foreach (var key2 in keys)
                {
                    dumpStr += string.Format(formatStr, matrix.get(key1, key2));
                }

                dumpStr += "\r\n";
            }

            File.WriteAllText(dumpFilename, dumpStr);
            Console.WriteLine(dumpStr);
        }

        private void DumpUnigrams(string dumpFilename)
        {
            var keys = this.unigramCounts.Keys.OrderBy(i => i);
            string formatStr = "{0,3} ";


            string dumpStr = "";

            foreach (var key in keys)
                dumpStr += key+";"+this.unigramCounts[key]+"\r\n";

            File.WriteAllText(dumpFilename, dumpStr);
            
        }
    }
}
