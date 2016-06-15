using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Detective.Search.Algorithms
{
    public class SimpleMatrix
    {
        public SimpleMatrix()
        {
                
        }

        public SimpleMatrix(string matrixFilename)
        {
            BuildMatrix(matrixFilename);
        }

        private void BuildMatrix(string matrixFilename)
        {
            var delim = " ".ToCharArray();
            var lines = File.ReadAllLines(matrixFilename);
            var alphabet = lines[0].Split(delim, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var values = line.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                var ch1 = values[0][0];
                for (int j = 1; j < values.Length; j++)
                {
                    var ch2 = alphabet[j-1][0];
                    var count = int.Parse(values[j]);
                    this.set(ch1, ch2, count);
                }
            }

        }

        private Dictionary<char, Dictionary<char, double>> matrix = new Dictionary<char, Dictionary<char, double>>();

        public void substract(char ch1, char ch2, double freq)
        {
            Dictionary<char, double> freqDict = null;

            if (matrix.ContainsKey(ch1))
                freqDict = matrix[ch1];
            else
                freqDict = new Dictionary<char, double>();

            if (freqDict.ContainsKey(ch2) == false)
                freqDict[ch2] = 0;

            freqDict[ch2] -= freq;

            freqDict[ch2] = Math.Max(freqDict[ch2], 0);

            matrix[ch1] = freqDict;
        }

        public void add(char ch1, char ch2, double freq)
        {
            Dictionary<char, double> freqDict = null;

            if (matrix.ContainsKey(ch1))
                freqDict = matrix[ch1];
            else
                freqDict = new Dictionary<char, double>();

            if (freqDict.ContainsKey(ch2) == false)
                freqDict[ch2] = 0;

            freqDict[ch2] += freq;

            matrix[ch1] = freqDict;
        }

        public void set(char ch1, char ch2, double freq)
        {
            Dictionary<char, double> freqDict = null;

            if (matrix.ContainsKey(ch1))
                freqDict = matrix[ch1];
            else
                freqDict = new Dictionary<char, double>();

            freqDict[ch2] = freq;

            matrix[ch1] = freqDict;
        }

        public double get(char ch1, char ch2)
        {
            if (matrix.ContainsKey(ch1) == false)
                return 0;

            var freqDict = matrix[ch1];

            if (freqDict.ContainsKey(ch2) == false)
                return 0;

            return freqDict[ch2];
        }

        public char[] getKeys()
        {
            return matrix.Keys.ToArray();
        }
    }
}
