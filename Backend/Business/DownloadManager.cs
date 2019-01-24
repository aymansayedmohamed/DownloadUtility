using DataAccess;
using IBusiness;
using IDataAccess;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class DownloadManager : IDownloadManager
    {
        private readonly IParser parser;
        private readonly IDownloadStrategyFactory downloadStrategyFactory;
        IRepository<DownloadedFile> repoDownloadedFile;

        public DownloadManager(IParser parser, IDownloadStrategyFactory downloadStrategyFactory, IRepository<DownloadedFile> repoDownloadedFile)
        {
            this.parser = parser;
            this.downloadStrategyFactory = downloadStrategyFactory;
            this.repoDownloadedFile = repoDownloadedFile;
        }

        public void Process(string sources)
        {

            var delimiter = ConfigurationManager.AppSettings["Delimiter"];
            string sourcesParsingDelimiter = string.IsNullOrWhiteSpace(delimiter) ? "," : delimiter ; 

            List<string> soursesUrls = parser.Parse(sources, sourcesParsingDelimiter).ToList();

            Parallel.ForEach(soursesUrls, url =>
            {

                IDownloadStrategy downloadStrategy = downloadStrategyFactory.Build(url);

                var localDestinationPath = downloadStrategy.Download(url);

                repoDownloadedFile.Add(new DownloadedFile()
                {
                    FileRemotePath = url,
                    LocalPath = localDestinationPath,
                    ProcessingStatus = 1,
                });

                repoDownloadedFile.SaveChanges();
            });

        }
    }
}
