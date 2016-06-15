using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Data;
using Detective.DataModel;
using Detective.Search.Rank;

namespace Detective.Query.Utils
{
    public static class Extensions
    {
        public static SearchItem[] ToSearchItem(this ScoredMatch[] scoredMatches)
        {
            List<SearchItem> searchItems = new List<SearchItem>();

            if (scoredMatches == null)
                return searchItems.ToArray();

            foreach (var record in scoredMatches)
            {
                var fullname = SQLDB.GetName(record.Id, record.NameIndex);
                var sdnItem = SQLDB.GetSDN(record.Id);
                var searchItem = new SearchItem() { Uid = record.Id, NameIndex = record.NameIndex, Fullname = fullname, SDN = sdnItem, Score = record.Score};
                searchItems.Add(searchItem);
            }

            return searchItems.ToArray();
        }
    }
}
