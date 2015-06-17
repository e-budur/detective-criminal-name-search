using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Search.Algorithms
{
    public class ConfusionMatrixBuilder
    {
        private ConfusionMatrix confusionMatrix;


        public ConfusionMatrixBuilder()
        {
            confusionMatrix = new ConfusionMatrix();
        }

        public void Build(
            string unigramFilename, 
            string bigramMatrixFilename,
            string substituionMatrixFilename,
            string insertionMatrixFilename,
            string deletionMatrixFilename
            )
        {
            BuildUnigramFrequencies(unigramFilename);
            BuildBigramFrequencies(bigramMatrixFilename);
            BuildSubstituionMatrix(substituionMatrixFilename);
            BuildInsertionMatrix(insertionMatrixFilename);
            BuildDeletionMatrix(deletionMatrixFilename);
        }

        private void BuildBigramFrequencies(string bigramMatrixFilename)
        {
            var matrix = new SimpleMatrix(bigramMatrixFilename);
            this.confusionMatrix.SetBigramMatrix(matrix);
        }

        private void BuildDeletionMatrix(string deletionMatrixFilename)
        {
            var matrix = new SimpleMatrix(deletionMatrixFilename);
            this.confusionMatrix.SetDeletionMatrix(matrix);
        }

        private void BuildInsertionMatrix(string insertionMatrixFilename)
        {
            var matrix = new SimpleMatrix(insertionMatrixFilename);
            this.confusionMatrix.SetInsertionMatrix(matrix);
        }

        private void BuildSubstituionMatrix(string substituionMatrixFilename)
        {
            var matrix = new SimpleMatrix(substituionMatrixFilename);
            this.confusionMatrix.SetSubstituionMatrix(matrix);
        }

        private void BuildUnigramFrequencies(string unigramFilename)
        {
            var delim = ";".ToCharArray();
            var lines = File.ReadAllLines(unigramFilename);
            foreach (var line in lines)
            {
                var parameters = line.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                char ch = parameters[0][0];
                int freq = int.Parse(parameters[1]);

                confusionMatrix.SetUnigram(ch, freq);
            }
        }

        public ConfusionMatrix GetConfusionMatrix()
        {
            return this.confusionMatrix;
        }
    }
}
