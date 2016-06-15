using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Detective.DataModel
{
    [DataContract]
    public class SearchItem
    {
        [DataMember]
        public int Uid;

        [DataMember]
        public int NameIndex;

        [DataMember]
        public string Fullname;

        [DataMember]
        public int Score;

        [DataMember]
        public SDNItem SDN;
    }
}
