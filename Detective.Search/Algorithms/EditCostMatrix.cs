using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Detective.Commons.Utils;

namespace Detective.Search.Algorithms
{
    public class EditCostMatrix
    {

        public static EditCostMatrix Instance;

        private ConfusionMatrix confusionMatrix;
        private double minProb = 0.2;
        public double[,] deletionProbs;
        public double[,] insertionProbs;
        public double[,] substitutionProbs;
        public double[,] transpositionProbs;
        public double[,] bigramProbs;


        public const int POWER_OFF_TRANSPOSITION = 90;
        public const int POWER_OFF_SUBSTITUTION = 30;
        public const int POWER_OFF_DELETION = 1;
        public const int POWER_OFF_INSERTION = 30;
        public const int POWER_OFF_BIGRAM = 5;


        private EditCostMatrix(ConfusionMatrix confusionMatrix)
        {
            this.confusionMatrix = confusionMatrix;

            initMatrices();
            calculateInsertionProbs();
            calculateSubstitutionProbs();
            calculateDeletionProbs();
            calculateTranspositionProbs();
            calculateBigramProbs();
        }

        private void initMatrices()
        {
            int n = this.confusionMatrix.GetDistinctUnigramCount();
            this.deletionProbs = new double[n, n];
            this.insertionProbs = new double[n, n];
            this.substitutionProbs = new double[n, n];
            this.transpositionProbs = new double[n, n];
            this.bigramProbs = new double[n, n];
        }

        private void calculateBigramProbs()
        {
            var keys = confusionMatrix.GetDistinctUnigrams();

            int distinctUnigramCount = confusionMatrix.GetDistinctUnigramCount();

            foreach (var ch1 in keys)
            {
                foreach (var ch2 in keys)
                {
                    var errorCount = confusionMatrix.GetDeletion(ch1, ch2);
                    double bigramCount = confusionMatrix.GetBigramCount(ch1, ch2);
                    double unigramCount = confusionMatrix.GetUnigramCount(ch2);
                    var prob = calculateProb(bigramCount, unigramCount, distinctUnigramCount, POWER_OFF_BIGRAM);

                    if (prob < minProb)
                        prob = minProb;

                    int i1 = ch1 - 'a';
                    int i2 = ch2 - 'a';

                    int len = transpositionProbs.GetLength(0);

                    if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                        continue;
                    else
                        bigramProbs[i1, i2] = prob;

                }
            }
        }

        private void calculateDeletionProbs()
        {
            var keys = confusionMatrix.GetDistinctUnigrams();

            int distinctUnigramCount = confusionMatrix.GetDistinctUnigramCount();

            foreach (var ch1 in keys)
            {
                foreach (var ch2 in keys)
                {
                    var errorCount = confusionMatrix.GetDeletion(ch1, ch2);
                    double bigramCount = confusionMatrix.GetBigramCount(ch1, ch2);
                    var prob = calculateProb(errorCount, bigramCount, distinctUnigramCount, POWER_OFF_DELETION);

                    if (prob < minProb)
                        prob = minProb;

                    int i1 = ch1 - 'a';
                    int i2 = ch2 - 'a';

                    int len = transpositionProbs.GetLength(0);

                    if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                        continue;
                    else
                        deletionProbs[i1, i2] = prob;
                }
            }
            deletionProbs['u' - 'a', 's' - 'a'] = 0.3;
            deletionProbs['s' - 'a', 's' - 'a'] = 0.3;
            deletionProbs['o' - 'a', 's' - 'a'] = 0.3;


        }

        private void calculateTranspositionProbs()
        {
            var keys = confusionMatrix.GetDistinctUnigrams();

            int distinctUnigramCount = confusionMatrix.GetDistinctUnigramCount();

            foreach (var ch1 in keys)
            {
                foreach (var ch2 in keys)
                {
                    var errorCount = confusionMatrix.GetTransposition(ch1, ch2);
                    double bigramCount = confusionMatrix.GetBigramCount(ch1, ch2);
                    var prob = calculateProb(errorCount, bigramCount, distinctUnigramCount, POWER_OFF_TRANSPOSITION);

                    if (prob < minProb)
                        prob = minProb;

                    int i1 = ch1 - 'a';
                    int i2 = ch2 - 'a';

                    int len = transpositionProbs.GetLength(0);

                    if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                        continue;
                    else
                        transpositionProbs[i1, i2] = prob;


                }
            }
        }

        private void calculateSubstitutionProbs()
        {
            var keys = confusionMatrix.GetDistinctUnigrams();

            int distinctUnigramCount = confusionMatrix.GetDistinctUnigramCount();

            foreach (var ch1 in keys)
            {
                double unigramCount = confusionMatrix.GetUnigramCount(ch1);

                foreach (var ch2 in keys)
                {
                    var errorCount = confusionMatrix.GetSubstitution(ch1, ch2);

                    var prob = calculateProb(errorCount, unigramCount, distinctUnigramCount, POWER_OFF_SUBSTITUTION);

                    if (prob < minProb)
                        prob = minProb;

                    int i1 = ch1 - 'a';
                    int i2 = ch2 - 'a';

                    int len = substitutionProbs.GetLength(0);

                    if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                        continue;
                    else
                        substitutionProbs[i1, i2] = prob;
                }
            }

            substitutionProbs['o' - 'a', 'u' - 'a'] = minProb;
            substitutionProbs['u' - 'a', 'o' - 'a'] = minProb;
            substitutionProbs['b' - 'a', 'd' - 'a'] = minProb;
        }

        private void calculateInsertionProbs()
        {
            var keys = confusionMatrix.GetDistinctUnigrams();

            int distinctUnigramCount = confusionMatrix.GetDistinctUnigramCount();

            foreach (var ch1 in keys)
            {
                double unigramCount = confusionMatrix.GetUnigramCount(ch1);

                foreach (var ch2 in keys)
                {
                    var errorCount = confusionMatrix.GetInsertion(ch1, ch2);

                    var prob = calculateProb(errorCount, unigramCount, distinctUnigramCount, POWER_OFF_INSERTION);

                    if (prob < minProb)
                        prob = minProb;

                    int i1 = ch1 - 'a';
                    int i2 = ch2 - 'a';

                    int len = insertionProbs.GetLength(0);

                    if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                        continue;
                    else
                        insertionProbs[i1, i2] = prob;
                }
            }

            insertionProbs['s' - 'a', 's' - 'a'] = minProb;
        }

        private double calculateProb(double num, double denom, int distinctUnigramCount, int powerOff)
        {
            if (denom == 0)
                return 1;

            var prob = (num) / (denom);

            return Math.Pow(1 - prob, powerOff);
        }

        public static EditCostMatrix Build()
        {
            var confusionMatrix = BuildConfusionMatrix();
            var probMatrix = new EditCostMatrix(confusionMatrix);
            Logger.WriteInfo("Probability matrix loaded");
            Instance = probMatrix;
            return probMatrix;
        }

        private static ConfusionMatrix BuildConfusionMatrix()
        {
            var cmb = new ConfusionMatrixBuilder();
            cmb.Build(
                "C:/Program Files/GT Labs/Detective/Assets/unigram.txt",
                "C:/Program Files/GT Labs/Detective/Assets/bigramMatrix.txt",
                "C:/Program Files/GT Labs/Detective/Assets/substitutionMatrix.txt",
                "C:/Program Files/GT Labs/Detective/Assets/insertionMatrix.txt",
                "C:/Program Files/GT Labs/Detective/Assets/deletionMatrix.txt");
            var confusionMatrix = cmb.GetConfusionMatrix();

            return confusionMatrix;
        }


        public void Dump()
        {
            this.DumpMatrix("substitutionProbMatrix.txt", this.substitutionProbs);
            this.DumpMatrix("insertionProbMatrix.txt", this.insertionProbs);
            this.DumpMatrix("deletionProbMatrix.txt", this.deletionProbs);
            this.DumpMatrix("bigramProbMatrix.txt", this.bigramProbs);

            confusionMatrix.Dump();
        }

        private void DumpMatrix(string dumpFilename, double[,] matrix)
        {
            var keys = matrix.Length;
            string formatStr = "{0:0.0000} ";

            string dumpStr = string.Format(formatStr, " ");

            for (int i = 0; i < matrix.GetLength(0); i++)
                dumpStr += string.Format("{0}      ", (char)(i + 'a'));

            dumpStr += "\r\n";

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                dumpStr += string.Format(formatStr, (char)(i + 'a'));

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var val = matrix[i, j];

                    dumpStr += string.Format(formatStr, matrix[i, j]);
                }

                dumpStr += "\r\n";
            }

            File.WriteAllText(dumpFilename, dumpStr);
            Console.WriteLine(dumpStr);
        }



        public static double getInsertionProb(char ch1, char ch2)
        {
            if (ch1 == '\0' || ch2 == '\0')
                return 1;

            int i1 = ch1 - 'a';
            int i2 = ch2 - 'a';
            int len = Instance.insertionProbs.GetLength(0);

            if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                return 1;

            var prob = Instance.insertionProbs[i1, i2];
            return prob;

        }

        public static double getDeletionProb(char ch1, char ch2)
        {
            if (ch1 == '\0' || ch2 == '\0')
                return 1;

            int i1 = ch1 - 'a';
            int i2 = ch2 - 'a';
            int len = Instance.deletionProbs.GetLength(0);

            if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                return 1;

            var prob = Instance.deletionProbs[i1, i2];
            return prob;
        }

        public static double getSubstitutionProb(char ch1, char ch2)
        {
            if (ch1 == '\0' || ch2 == '\0')
                return 1;

            if (ch1 == ch2)
                return 0;

            int i1 = ch1 - 'a';
            int i2 = ch2 - 'a';
            int len = Instance.substitutionProbs.GetLength(0);

            if (i1 < 0 || i2 < 0 || i1 > len || i2 > len)
                return 1;

            var prob = Instance.substitutionProbs[i1, i2];
            return prob;
        }
    }
}
