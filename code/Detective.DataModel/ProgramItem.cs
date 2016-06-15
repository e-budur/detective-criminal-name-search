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
    public class ProgramItem
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [DataMember]
        public string program { get; set; }

        [NotMapped]
        public virtual ICollection<SDNItem> SDNs { get; set; }

        public ProgramItem()
        {
            SDNs = new List<SDNItem>();
            this.program = "";
        }
    }
}
