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
    public class IDItem : BaseItem
    {
        [DataMember]
        public string id_type { get; set; }
        [DataMember]
        public string id_number { get; set; }
        [DataMember]
        public string id_country { get; set; }
        [DataMember]
        public string issue_date { get; set; }
        [DataMember]
        public string expiration_date { get; set; }
        [NotMapped]
        public virtual ICollection<SDNItem> SDNs { get; set; }

        public IDItem()
        {
            SDNs = new List<SDNItem>();
            this.id_type = "";
            this.id_number = "";
            this.id_country = "";
            this.issue_date = "";
            this.expiration_date = "";
        }
    }
}
