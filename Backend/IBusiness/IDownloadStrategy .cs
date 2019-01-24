using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface IDownloadStrategy
    {
        bool IsMatch(String protocol);
        string Download(string source);
    }
}
