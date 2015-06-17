using Detective.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Detective.Feeder;
using EntityFramework.BulkInsert.Extensions;

namespace Detective.Data
{
    public class SQLDB
    {
        private static Dictionary<int, SDNItem> SDNItems = new Dictionary<int, SDNItem>();
        
        private static int nameCount;
        
        public static string GetName(int uid, int nameIndex)
        {
            if (!SDNItems.ContainsKey(uid))
                return string.Empty;

            var sdnItem = SDNItems[uid];

            if (nameIndex == 0)
                return sdnItem.first_name + " " + sdnItem.last_name;

            if (sdnItem.AkaList.Count < nameIndex)
                return string.Empty;

            return sdnItem.AkaList[nameIndex-1].first_name + " " + sdnItem.AkaList[nameIndex-1].last_name;
        }

        public static SDNItem GetSDN(int uid)
        {
            if (!SDNItems.ContainsKey(uid))
                return null;

            return SDNItems[uid];
        }

        public static int GetNameCount()
        {
            return nameCount;
        }

        public static int GetDocCount()
        {
            return SDNItems.Values.Count;
        }

        public static void ClearAllSDNs()
        {
            SDNItems.Clear();
        }
        public static void AddSDN(SDNItem sdnItem)
        {
            SDNItems[sdnItem.uid] = sdnItem;
            nameCount += sdnItem.IDList.Count + 1;
        }



        public static void ReloadMemoryDB()
        {
            SDNFileReader sdnFileReader = new SDNFileReader("https://www.treasury.gov/ofac/downloads/sdn.xml");
            int i = 0;
            foreach (var item in sdnFileReader.ReadSDNItem())
            {
                SQLDB.AddSDN(item);
                i++;
            }
            Console.WriteLine("SDNCount : " + i);
        }


        public static void ReloadDbTask()
        {
            Task.Factory.StartNew(ReloadDB).ContinueWith(ReloadDBFinished);
        }

        //event on db reload operation is completed
        public static void ReloadDBFinished(Task obj)
        {

        }

        private static object dbReloadLock = new object();
        private static bool dbReloadReady = false;

        private static void ReloadDB()
        {
            if (Monitor.TryEnter(dbReloadLock))
            {
                try
                {
                    using (var context = new BlackcheckEntities())
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        Trace.WriteLine(String.Format("Wipeout started: {0}", sw.ElapsedMilliseconds));
                        context.WipeOut();
                        Trace.WriteLine(String.Format("Wipeout finished: {0}", sw.ElapsedMilliseconds));

                        SDNFileReader sdnFileReader = new SDNFileReader("https://www.treasury.gov/ofac/downloads/sdn.xml", true);
                        Trace.WriteLine(String.Format("Insertion is started: {0}", sw.ElapsedMilliseconds));

                        #region bulk insert
                        BulkInsertOptions options = new BulkInsertOptions();
                        options.EnableStreaming = true;

                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.ProgramList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.IDList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.AkaList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.AddressList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.NationalityList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.CitizenshipList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.DateOfBirthItemList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.PlaceOfBirthList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().SelectMany(item => item.VesselInfoList), options);
                        context.BulkInsert(sdnFileReader.ReadSDNItem().Select(item => item), options);

                        int sdnIndex = 1;
                        int addressIndex = 1;
                        int akaIndex = 1;
                        int citizenshipIndex = 1;
                        int dateofbirthIndex = 1;
                        int idIndex = 1;
                        int nationalityIndex = 1;
                        int placeofbirthIndex = 1;
                        int programIndex = 1;
                        int vesselinfoIndex = 1;

                        DataTable dtAddress = new DataTable();
                        DataTable dtAka = new DataTable();
                        DataTable dtCitizenship = new DataTable();
                        DataTable dtDateofbirth = new DataTable();
                        DataTable dtID = new DataTable();
                        DataTable dtNationality = new DataTable();
                        DataTable dtPlaceofbirth = new DataTable();
                        DataTable dtProgram = new DataTable();
                        DataTable dtVesselinfo = new DataTable();

                        foreach (var item in sdnFileReader.ReadSDNItem())
                        {
                            context.BuildDataTable(dtAddress, item.AddressList.Count, "sdn_id", "address_id", sdnIndex, addressIndex);
                            context.BuildDataTable(dtAka, item.AkaList.Count, "sdn_id", "aka_id", sdnIndex, akaIndex);
                            context.BuildDataTable(dtCitizenship, item.CitizenshipList.Count, "sdn_id", "citizenship_id", sdnIndex, citizenshipIndex);
                            context.BuildDataTable(dtDateofbirth, item.DateOfBirthItemList.Count, "sdn_id", "dateofbirth_id", sdnIndex, dateofbirthIndex);
                            context.BuildDataTable(dtID, item.IDList.Count, "sdn_id", "id_id", sdnIndex, idIndex);
                            context.BuildDataTable(dtNationality, item.NationalityList.Count, "sdn_id", "nationality_id", sdnIndex, nationalityIndex);
                            context.BuildDataTable(dtPlaceofbirth, item.PlaceOfBirthList.Count, "sdn_id", "placeofbirth_id", sdnIndex, placeofbirthIndex);
                            context.BuildDataTable(dtProgram, item.ProgramList.Count, "sdn_id", "program_id", sdnIndex, programIndex);
                            context.BuildDataTable(dtVesselinfo, item.VesselInfoList.Count, "sdn_id", "vesselinfo_id", sdnIndex, vesselinfoIndex);

                            sdnIndex += 1;
                            addressIndex += item.AddressList.Count;
                            citizenshipIndex += item.CitizenshipList.Count;
                            dateofbirthIndex += item.DateOfBirthItemList.Count;
                            idIndex += item.IDList.Count;
                            nationalityIndex += item.NationalityList.Count;
                            placeofbirthIndex += item.PlaceOfBirthList.Count;
                            programIndex += item.ProgramList.Count;
                            vesselinfoIndex += item.VesselInfoList.Count;
                        }

                        context.BulkInsertCustom(dtAddress, "T_SDN_ADDRESS");
                        context.BulkInsertCustom(dtAka, "T_SDN_AKA");
                        context.BulkInsertCustom(dtCitizenship, "T_SDN_CITIZENSHIP");
                        context.BulkInsertCustom(dtDateofbirth, "T_SDN_DATEOFBIRTH");
                        context.BulkInsertCustom(dtID, "T_SDN_ID");
                        context.BulkInsertCustom(dtNationality, "T_SDN_NATIONALITY");
                        context.BulkInsertCustom(dtPlaceofbirth, "T_SDN_PLACEOFBIRTH");
                        context.BulkInsertCustom(dtProgram, "T_SDN_PROGRAM");
                        context.BulkInsertCustom(dtVesselinfo, "T_SDN_VESSELINFO");
                        #endregion

                        sw.Stop();
                        Trace.WriteLine(String.Format("Insertion is finished: {0}", sw.ElapsedMilliseconds));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Monitor.Exit(dbReloadLock);
                    dbReloadReady = true;
                }
            }
        }

    }
}
