using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IViewModels
{
    public interface IDownloadedFile
    {
         int Id { get; set; }
         string Source { get; set; }
         string Url { get; set; }
         Nullable<int> ProcessingStatusId { get; set; }
         string ProcessingStatus { get; set; }
         string FileType { get; set; }

    }
}
