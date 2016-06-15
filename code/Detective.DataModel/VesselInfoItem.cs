using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Detective.DataModel
{
    [DataContract]
    public class VesselInfoItem : BaseItem
    {
        [DataMember]
        public string call_sign { get; set; }
        [DataMember]
        public string vessel_type { get; set; }
        [DataMember]
        public string vessel_flag { get; set; }
        [DataMember]
        public string vessel_owner { get; set; }
        [DataMember]
        public int tonnage { get; set; }
        [DataMember]
        public int gross_registered_tonnage { get; set; }
        [NotMapped]
        public virtual ICollection<SDNItem> SDNs { get; set; }

        public VesselInfoItem()
        {
            SDNs = new List<SDNItem>();
            this.call_sign = "";
            this.vessel_type = "";
            this.vessel_flag = "";
            this.vessel_owner = "";
        }
    }
}
