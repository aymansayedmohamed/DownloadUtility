using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusiness.IHelpers
{
    public interface IIOHelper
    {
        string GetLocalDestinationPath(string remotePath, string localPath);

    }
}
