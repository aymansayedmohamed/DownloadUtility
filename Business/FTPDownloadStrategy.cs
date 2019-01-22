using DownloadUtilityLogger;
using IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class FTPDownloadStrategy : IDownloadStrategy
    {
        private readonly ILogger logger;
        private readonly string protocol = "FTP";

        public FTPDownloadStrategy(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsMatch(string protocol)
        {
            return protocol == this.protocol; 
        }

        public void Download(string source)
        {
            //Implement ftp download startgy    
            int x = 1;
            logger.AddInformationLog($"X = {x} source= {source}");

            x++;
            logger.AddInformationLog($"X = {x} source= {source}");

        }

    }
}
