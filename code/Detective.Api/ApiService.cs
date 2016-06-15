using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Api
{
    partial class ApiService : ServiceBase
    {
        public ServiceHost host = null;
        public ApiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (host != null)
                host.Close();
            Logger logger = LogManager.GetLogger("MyClassName");
            logger.Info("Onstart");
            host = new ServiceHost(typeof(DetectiveNameSearch));
            host.Open();
            logger.Info("Onstart end");
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
