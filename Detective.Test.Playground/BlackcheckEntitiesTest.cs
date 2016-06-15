using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.Data;
using Detective.Feeder;

namespace Detective.TestBed
{
    class BlackcheckEntitiesTest
    {
        public static void Main(string[] args)
        {
            var entities = new BlackcheckEntities();
            entities.WipeOut();

            //Example usage of SDN file reader
            SDNFileReader sdnFileReader = new SDNFileReader("https://www.treasury.gov/ofac/downloads/sdn.xml");
            foreach (var item in sdnFileReader.ReadSDNItem())
            {
                Console.WriteLine("{0}: {1} {2}", item.uid, item.first_name, item.last_name);

                try
                {
                    entities.T_SDN.Add(item);
                    entities.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ModelValidationException e)
                {
                    Console.WriteLine(e.Message);
                    foreach (var data in e.Data)
                    {
                        Console.WriteLine( data);
                    }
                    throw;
                }
                
            }

        }
    }
}
