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
    public class AkaItem : BaseItem
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string first_name { get; set; }
        [DataMember]
        public string last_name { get; set; }
        [NotMapped]
        public virtual ICollection<SDNItem> SDNs { get; set; }

        public AkaItem()
        {
            SDNs = new List<SDNItem>();
            this.type = "";
            this.category = "";
            this.first_name = "";
            this.last_name = "";
        }
    }
}
