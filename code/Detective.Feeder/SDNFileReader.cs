using Detective.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Detective.DataModel.SDNItem;

namespace Detective.Feeder
{
    public class SDNFileReader
    {

        #region Fields
        private string targetURL = "";
        private bool useCache = false;
        private XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();

        private string targetPath = "";
        #endregion

        #region Constructor
        /// <summary>
        /// Creates an SDN File Reader object. Objects can be read by ReadSDNItem() method which returns IEnumarable.
        /// </summary>
        /// <param name="targetURL">URL of SDN file (E.g. https://www.treasury.gov/ofac/downloads/sdn.xml")</param>
        /// <param name="useCache">If true, reader will try to download whole file if file doesn't exist in temp path (causes delay on initialization). If false, reader will return results on the fly.</param>
        /// <param name="xmlReaderSettings">XML Reader settings can be supplied for reading process. (E.g. localizations, charset etc...) </param>
        public SDNFileReader(string targetURL, bool useCache = false, XmlReaderSettings xmlReaderSettings = null)
        {
            this.targetURL = targetURL;
            this.useCache = useCache;

            if (xmlReaderSettings == null)
            {
                this.xmlReaderSettings.IgnoreWhitespace = true;
            }
            else
            {
                this.xmlReaderSettings = xmlReaderSettings;
            }

            Init();
        }
        #endregion

        #region Methods

        #region Operations
        private void Init()
        {
            Uri uri = new Uri(this.targetURL);
            this.targetPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(uri.AbsolutePath));

            if (this.useCache)
            {
                if (File.Exists(this.targetPath))
                {
                    File.Delete(this.targetPath);
                }

                DownladTargetFile(this.targetURL, this.targetPath);
            }
        }

        private void DownladTargetFile(string targetURL, string targetPath)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(targetURL, targetPath);
        }
        #endregion

        #region Business
        //TODO: Will be used yield return, list is used for debugging purposes DONE
        //TODO: Constant strings may be replaced with model-related variables
        public IEnumerable<SDNItem> ReadSDNItem()
        {
            string readPath = this.useCache ? this.targetPath : this.targetURL;

            using (XmlReader xmlReader = XmlReader.Create(readPath, xmlReaderSettings))
            {
                xmlReader.ReadToFollowing("sdnEntry");
                do
                {
                    SDNItem sdnItem = new SDNItem();
                    XElement xelem = (XElement)XNode.ReadFrom(xmlReader);

                    foreach (var item in xelem.Elements())
                    {
                        switch (item.Name.LocalName)
                        {
                            case "uid":
                                {
                                    sdnItem.uid = int.Parse(item.Value);
                                    break;
                                }
                            case "firstName":
                                {
                                    sdnItem.first_name = item.Value;
                                    break;
                                }
                            case "lastName":
                                {
                                    sdnItem.last_name = item.Value;
                                    break;
                                }
                            case "title":
                                {
                                    sdnItem.title = item.Value;
                                    break;
                                }
                            case "sdnType":
                                {
                                    sdnItem.sdn_type = item.Value;
                                    break;
                                }
                            case "remarks":
                                {
                                    sdnItem.remarks = item.Value;
                                    break;
                                }
                            case "programList":
                                {
                                    XElement itemRoot = new XElement("programItem", item);

                                    foreach (var programItemList in itemRoot.Elements())
                                    {
                                        ProgramItem programItem = new ProgramItem();

                                        foreach (var programItemProperty in programItemList.Elements())
                                        {
                                            switch (programItemProperty.Name.LocalName)
                                            {
                                                case "program":
                                                    programItem.program = programItemProperty.Value;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }

                                        sdnItem.ProgramList.Add(programItem);
                                    }
                                    break;
                                }

                            case "idList":
                                {
                                    foreach (var idItemList in item.Elements())
                                    {
                                        IDItem idItem = new IDItem();

                                        foreach (var idItemProperty in idItemList.Elements())
                                        {
                                            switch (idItemProperty.Name.LocalName)
                                            {
                                                case "uid":
                                                    idItem.uid = int.Parse(idItemProperty.Value);
                                                    break;
                                                case "idType":
                                                    idItem.id_type = idItemProperty.Value;
                                                    break;
                                                case "idNumber":
                                                    idItem.id_number = idItemProperty.Value;
                                                    break;
                                                case "idCountry":
                                                    idItem.id_country = idItemProperty.Value;
                                                    break;
                                                case "issueDate":
                                                    idItem.issue_date = idItemProperty.Value;
                                                    break;
                                                case "expirationDate":
                                                    idItem.expiration_date = idItemProperty.Value;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }

                                        sdnItem.IDList.Add(idItem);
                                    }
                                    break;
                                }
                            case "akaList":
                                {
                                    foreach (var akaItemList in item.Elements())
                                    {
                                        AkaItem akaItem = new AkaItem();

                                        foreach (var akaItemProperty in akaItemList.Elements())
                                        {
                                            switch (akaItemProperty.Name.LocalName)
                                            {
                                                case "uid":
                                                    akaItem.uid = int.Parse(akaItemProperty.Value);
                                                    break;
                                                case "type":
                                                    akaItem.type = akaItemProperty.Value;
                                                    break;
                                                case "category":
                                                    akaItem.category = akaItemProperty.Value;
                                                    break;
                                                case "firstName":
                                                    akaItem.first_name = akaItemProperty.Value;
                                                    break;
                                                case "lastName":
                                                    akaItem.last_name = akaItemProperty.Value;
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                        sdnItem.AkaList.Add(akaItem);
                                    }
                                    break;
                                }
                            case "addressList":
                                {
                                    foreach (var addressItemList in item.Elements())
                                    {
                                        AddressItem addressItem = new AddressItem();

                                        foreach (var addressItemProperty in addressItemList.Elements())
                                        {
                                            switch (addressItemProperty.Name.LocalName)
                                            {
                                                case "uid":
                                                    addressItem.uid = int.Parse(addressItemProperty.Value);
                                                    break;
                                                case "address1":
                                                    addressItem.address1 = addressItemProperty.Value;
                                                    break;
                                                case "address2":
                                                    addressItem.address2 = addressItemProperty.Value;
                                                    break;
                                                case "address3":
                                                    addressItem.address3 = addressItemProperty.Value;
                                                    break;
                                                case "city":
                                                    addressItem.city = addressItemProperty.Value;
                                                    break;
                                                case "stateOrProvince":
                                                    addressItem.state_or_province = addressItemProperty.Value;
                                                    break;
                                                case "postalCode":
                                                    addressItem.postal_code = addressItemProperty.Value;
                                                    break;
                                                case "country":
                                                    addressItem.country = addressItemProperty.Value;
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                        sdnItem.AddressList.Add(addressItem);
                                    }
                                    break;
                                }
                            case "nationalityList":
                                {
                                    foreach (var nationalityItemList in item.Elements())
                                    {
                                        NationalityItem nationalityItem = new NationalityItem();

                                        foreach (var nationalityItemProperty in nationalityItemList.Elements())
                                        {
                                            switch (nationalityItemProperty.Name.LocalName)
                                            {
                                                case "uid":
                                                    nationalityItem.uid = int.Parse(nationalityItemProperty.Value);
                                                    break;
                                                case "country":
                                                    nationalityItem.country = nationalityItemProperty.Value;
                                                    break;
                                                case "mainEntry":
                                                    nationalityItem.main_entry = bool.Parse(nationalityItemProperty.Value);
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                        sdnItem.NationalityList.Add(nationalityItem);
                                    }
                                    break;
                                }
                            case "citizenshipList":
                                {
                                    foreach (var citizenshipItemList in item.Elements())
                                    {
                                        CitizenshipItem citizenshipItem = new CitizenshipItem();

                                        foreach (var citizenshipItemProperty in citizenshipItemList.Elements())
                                        {
                                            switch (citizenshipItemProperty.Name.LocalName)
                                            {
                                                case "uid":
                                                    citizenshipItem.uid = int.Parse(citizenshipItemProperty.Value);
                                                    break;
                                                case "country":
                                                    citizenshipItem.country = citizenshipItemProperty.Value;
                                                    break;
                                                case "mainEntry":
                                                    citizenshipItem.main_entry = bool.Parse(citizenshipItemProperty.Value);
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                        sdnItem.CitizenshipList.Add(citizenshipItem);
                                    }
                                    break;
                                }
                            case "dateOfBirthList":
                                {
                                    foreach (var dateOfBirthItemList in item.Elements())
                                    {
                                        DateOfBirthItem dateOfBirthItem = new DateOfBirthItem();

                                        foreach (var dateOfBirthItemProperty in dateOfBirthItemList.Elements())
                                        {
                                            switch (dateOfBirthItemProperty.Name.LocalName)
                                            {
                                                case "uid":
                                                    dateOfBirthItem.uid = int.Parse(dateOfBirthItemProperty.Value);
                                                    break;
                                                case "dateOfBirth":
                                                    dateOfBirthItem.date_of_birth = dateOfBirthItemProperty.Value;
                                                    break;
                                                case "mainEntry":
                                                    dateOfBirthItem.main_entry = bool.Parse(dateOfBirthItemProperty.Value);
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                        sdnItem.DateOfBirthItemList.Add(dateOfBirthItem);
                                    }
                                    break;
                                }
                            case "placeOfBirthList":
                                {
                                    foreach (var placeOfBirthItemList in item.Elements())
                                    {
                                        PlaceOfBirthItem placeOfBirthItem = new PlaceOfBirthItem();

                                        foreach (var placeOfBirthItemProperty in placeOfBirthItemList.Elements())
                                        {
                                            switch (placeOfBirthItemProperty.Name.LocalName)
                                            {
                                                case "uid":
                                                    placeOfBirthItem.uid = int.Parse(placeOfBirthItemProperty.Value);
                                                    break;
                                                case "placeOfBirth":
                                                    placeOfBirthItem.place_of_birth = placeOfBirthItemProperty.Value;
                                                    break;
                                                case "mainEntry":
                                                    placeOfBirthItem.main_entry = bool.Parse(placeOfBirthItemProperty.Value);
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                        sdnItem.PlaceOfBirthList.Add(placeOfBirthItem);
                                    }
                                    break;
                                }
                            case "vesselInfo":
                                {
                                    XElement itemRoot = new XElement("vesselInfoItem", item);

                                    foreach (var vesselInfoItemList in itemRoot.Elements())
                                    {
                                        VesselInfoItem vesselInfoItem = new VesselInfoItem();

                                        foreach (var vesselInfoItemProperty in vesselInfoItemList.Elements())
                                        {
                                            switch (vesselInfoItemProperty.Name.LocalName)
                                            {
                                                case "callSign":
                                                    vesselInfoItem.call_sign = vesselInfoItemProperty.Value;
                                                    break;
                                                case "vesselType":
                                                    vesselInfoItem.vessel_type = vesselInfoItemProperty.Value;
                                                    break;
                                                case "vesselFlag":
                                                    vesselInfoItem.vessel_flag = vesselInfoItemProperty.Value;
                                                    break;
                                                case "vesselOwner":
                                                    vesselInfoItem.vessel_owner = vesselInfoItemProperty.Value;
                                                    break;
                                                case "tonnage":
                                                    vesselInfoItem.tonnage = int.Parse(vesselInfoItemProperty.Value);
                                                    break;
                                                case "grossRegisteredTonnage":
                                                    vesselInfoItem.gross_registered_tonnage = int.Parse(vesselInfoItemProperty.Value);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }

                                        sdnItem.VesselInfoList.Add(vesselInfoItem);
                                    }
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }

                    yield return sdnItem;

                } while (xmlReader.NodeType != XmlNodeType.EndElement);
            }
        }
        #endregion

        #endregion

    }
}