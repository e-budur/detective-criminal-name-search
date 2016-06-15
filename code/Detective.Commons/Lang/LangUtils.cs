using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Commons.Lang
{
    public static class LangExtensions
    {
        public static string NormalizeString(this string input)
        {
            return input.ToLower(new CultureInfo("en-US")).Trim();
        }

        public static string[] SplitString(this string input)
        {
            return input.Split(" ,?-;:./\\()[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
