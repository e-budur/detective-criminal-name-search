using Detective.Api;
using Detective.Data;
using Detective.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Query.Result
{
    public class CandidateRecordSetDecoder
    {
        private CandidateRecordSet candidateRecordSet;

        public CandidateRecordSetDecoder(CandidateRecordSet candidateRecordSet)
        {
            this.candidateRecordSet = candidateRecordSet;
        }

        public SearchItem[] Decode()
        {
            List<SearchItem> searchItems = new List<SearchItem>();
            foreach (var record in candidateRecordSet)
            {
                foreach (var name in record.Value)
                {
                    var fullname = SQLDB.GetName(record.Key, name.Key);
                    var sdnItem = SQLDB.GetSDN(record.Key);
                    var searchItem = new SearchItem() { Uid = record.Key, NameIndex = name.Key, Fullname = fullname, SDN = sdnItem };
                    searchItems.Add(searchItem);
                }
            }

            return searchItems.ToArray();
        }
    }
}
