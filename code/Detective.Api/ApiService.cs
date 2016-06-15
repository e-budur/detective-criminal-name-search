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
using Detective.Data;
using Detective.Index.Trie;
using Detective.Commons.Utils;
using Detective.Search.Algorithms;

namespace Detective.Api
{
    partial class ApiService : ServiceBase
    {
        public ServiceHost host = null;
        public ApiService()
        {
            InitializeComponent();
        }

        protected void OnInit()
        {
            Logger.WriteInfo("inititializing index");
            TrieIndexer.Index();
            Logger.WriteInfo("inititialized index");

            Logger.WriteInfo("reloading db");
            //SQLDB.ReloadDbTask();
            Logger.WriteInfo("reloaded db");

            Logger.WriteInfo("reloading cost matrix");
            EditCostMatrix.Build();
            Logger.WriteInfo("reloaded cost matrix");

        }
        protected override void OnStart(string[] args)
        {
            host?.Close();

            Logger.WriteInfo("Onstart");
            OnInit();

            Logger.WriteInfo("Starting WCF service");

            host = new ServiceHost(typeof(DetectiveNameSearch));
            host.Open();
            Logger.WriteInfo("Started WCF service");
        }

        protected override void OnStop()
        {
            if(host != null && host.State != CommunicationState.Closed)
                host.Close();
        }
    }
}
