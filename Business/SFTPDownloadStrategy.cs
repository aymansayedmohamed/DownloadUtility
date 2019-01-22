using IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class SFTPDownloadStrategy : IDownloadStrategy
    {
        private readonly string protocol = "SFTP";

        public bool IsMatch(string protocol)
        {
            return protocol == this.protocol; 
        }

        public void Download(string source)
        {
            //Implement ftp download startgy
        }
       
    }
}
