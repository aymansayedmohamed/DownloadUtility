using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadUtilityLogger
{
    public interface ILogger
    {
        void AddErrorLog(string msg);

        void AddErrorLog(string msg, Exception ex);

        void AddErrorLog(Exception ex);

        void AddInformationLog(string msg);

        void AddWarningLog(string msg);
    
    }
}
