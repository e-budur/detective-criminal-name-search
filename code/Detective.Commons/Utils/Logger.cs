using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Detective.Commons.Utils
{
    public class Logger
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public static void WriteInfo(string message)
        {
            logger.Info(message);
        }
    }
}
