using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Search.Utils
{
    public class GlobalSearchParams
    {
        public static double DEFAULT_MAX_EDIT_COST = 1.2;
        public static double[] MAX_EDIT_COSTS = {0, 0, 0, 0, 0.3, 0.4};
    }
}
