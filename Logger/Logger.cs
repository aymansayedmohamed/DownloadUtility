using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadUtilityLogger
{
    public class Logger : ILogger
    {

        private static log4net.ILog Log { get; set; }

        public Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }


        public void AddErrorLog(string msg)
        {
            Log.Error(msg);
        }

        public void AddErrorLog(string msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public void AddErrorLog(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public void AddInformationLog(string msg)
        {
            Log.Info(msg);
        }

        public void AddWarningLog(string msg)
        {
            Log.Warn(msg);
        }


    }
}
