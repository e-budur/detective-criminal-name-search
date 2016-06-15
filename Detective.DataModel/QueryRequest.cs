using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Api
{

    [DataContract]
    public class QueryRequest
    {
        [DataMember]
        public string QueryString;
    }
}
