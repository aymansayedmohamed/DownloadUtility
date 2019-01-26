using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDomainModels
{
    public interface IDownloadedFile
    {
         int Id { get; set; }
         string FileRemotePath { get; set; }
         string LocalPath { get; set; }
         Nullable<int> ProcessingStatus { get; set; }

    }
}
