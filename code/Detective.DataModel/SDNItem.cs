using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Detective.DataModel
{

    //TODO:  PlaceOf, BirthDayOf and any other properties will be added to model. DONE
    //TODO:  Types will be replaced according to XSD (instead of strings) DONE

    [DataContract]
    public class SDNItem
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [DataMember]
        public int uid { get; set; }
        [DataMember]
        public string first_name { get; set; }
        [DataMember]
        public string last_name { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string sdn_type { get; set; }
        [DataMember]
        public string remarks { get; set; }

        [DataMember]
        public IList<ProgramItem> ProgramList { get; set; }
        [DataMember]
        public IList<IDItem> IDList { get; set; }
        [DataMember]
        public IList<AkaItem> AkaList { get; set; }
        [DataMember]
        public IList<AddressItem> AddressList { get; set; }
        [DataMember]
        public IList<NationalityItem> NationalityList { get; set; }
        [DataMember]
        public IList<CitizenshipItem> CitizenshipList { get; set; }
        [DataMember]
        public IList<DateOfBirthItem> DateOfBirthItemList { get; set; }
        [DataMember]
        public IList<PlaceOfBirthItem> PlaceOfBirthList { get; set; }
        [DataMember]
        public IList<VesselInfoItem> VesselInfoList { get; set; }

        public SDNItem()
        {
            this.ProgramList = new List<ProgramItem>();
            this.AddressList = new List<AddressItem>();
            this.AkaList = new List<AkaItem>();
            this.DateOfBirthItemList = new List<DateOfBirthItem>();
            this.IDList = new List<IDItem>();
            this.NationalityList = new List<NationalityItem>();
            this.PlaceOfBirthList = new List<PlaceOfBirthItem>();
            this.VesselInfoList = new List<VesselInfoItem>();
            this.CitizenshipList = new List<CitizenshipItem>();

            this.first_name = "";
            this.last_name = "";
            this.remarks = "";
            this.sdn_type = "";
            this.title = "";
        }
    }
}
