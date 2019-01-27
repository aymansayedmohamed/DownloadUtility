using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ViewModels.Constants;

namespace IBusiness.IHelpers
{
    public interface IIOHelper
    {
        string GetLocalDestinationPath(string remotePath, string localPath);
        FileType GetFileType(string extention);


    }
}
