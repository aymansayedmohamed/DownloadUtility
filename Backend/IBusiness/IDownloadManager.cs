using IViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace IBusiness
{
    public interface IDownloadManager
    {
        void Process(string sources);
        IQueryable<IDownloadedFile> GetReadyForProcessingFiles();
        IDownloadedFile ApproveFile(IDownloadedFile file);
        IDownloadedFile RejectFile(IDownloadedFile file);
    }
}
