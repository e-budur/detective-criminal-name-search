using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Search.Algorithms;

namespace Detective.TestBed
{
    public class ConfusionMatrixBuilderTest
    {
        public static void Main(string[] args)
        {
            EditCostMatrix.Instance = EditCostMatrix.Build();

            EditCostMatrix.Instance.Dump();
        }
    }
}
