using Detective.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Api
{
    [DataContract]
    public class QueryResult
    {
        [DataMember]
        public string Query;
        
        [DataMember]
        public SearchItem[] SearchItems;
        [DataMember]
        public int SearchItemCount;
        [DataMember]
        public int AllNameCount;
        [DataMember]
        public string SearchTime;
        [DataMember]
        public string Message;
        [DataMember]
        public string ResultCode;
        [DataMember]
        public string ResultCodeExplanation;

    }
}
