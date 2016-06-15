using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Api
{
    class WCFTestBed
    {
        public static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(DetectiveNameSearch));
            host.Open();
        }
    }
}
