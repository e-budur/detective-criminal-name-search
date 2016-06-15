using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Detective.DataModel
{
    [DataContract]
    public class AddressItem : BaseItem
    {
        [DataMember]
        public string address1 { get; set; }
        [DataMember]
        public string address2 { get; set; }
        [DataMember]
        public string address3 { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string state_or_province { get; set; }
        [DataMember]
        public string postal_code { get; set; }
        [DataMember]
        public string country { get; set; }
        [NotMapped]
        public virtual ICollection<SDNItem> SDNs { get; set; }

        public AddressItem()
        {
            SDNs = new List<SDNItem>();
            this.address1 = "";
            this.address2 = "";
            this.address3 = "";
            this.city = "";
            this.state_or_province = "";
            this.postal_code = "";
            this.country = "";
        }
    }
}
