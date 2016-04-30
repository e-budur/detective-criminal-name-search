using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Data
{
    public class SQLDB
    {
        private static int nameCount;

        private static Dictionary<int, Dictionary<int, string>> Names = new Dictionary<int, Dictionary<int, string>>();

        public static void AddName(int uid, int nameIndex, string name)
        {
            var entityNames = new Dictionary<int, string>();

            if (Names.ContainsKey(uid))
                entityNames = Names[uid];
            else
                entityNames = new Dictionary<int, string>();

            if (!entityNames.ContainsKey(nameIndex))
            {
                entityNames[nameIndex] = name;
                nameCount++;
            }

            Names[uid] = entityNames;
        }

        public static string GetName(int uid, int nameIndex)
        {
            if (!Names.ContainsKey(uid))
                return string.Empty;

            if (!Names[uid].ContainsKey(nameIndex))
                return string.Empty;

            return Names[uid][nameIndex];

        }

        public static int GetNameCount()
        {
            return nameCount;
        }
    }
}
