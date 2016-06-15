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
    public class PlaceOfBirthItem : BaseItem
    {
        [DataMember]
        public string place_of_birth { get; set; }
        [DataMember]
        public bool main_entry { get; set; }
        [NotMapped]
        public virtual ICollection<SDNItem> SDNs { get; set; }

        public PlaceOfBirthItem()
        {
            SDNs = new List<SDNItem>();
            this.place_of_birth = "";
        }
    }
}
