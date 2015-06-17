using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Searchers
{
    public class EditCost
    {
        public double InsertCost;
        public double SubstituionCost;
        public double DeletionCost;
        public double MinCost
        {
            get
            {
                return Math.Min(Math.Min(InsertCost, SubstituionCost), DeletionCost);
            }
        }
        public char PreviousQueryChar;
        public char CurrentQueryChar;

        public char PreviousNameChar;
        public char CurrentNameChar;
    }
}
