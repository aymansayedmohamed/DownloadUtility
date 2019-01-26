using IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DownloadedFile : IDownloadedFile
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public Nullable<int> ProcessingStatusId { get; set; }
        public string ProcessingStatus { get; set; }

    }
}
