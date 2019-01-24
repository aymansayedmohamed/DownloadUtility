using IBusiness;
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

        public DownloadManager(IParser parser, IDownloadStrategyFactory downloadStrategyFactory)
        {
            this.parser = parser;
            this.downloadStrategyFactory = downloadStrategyFactory;
        }

        public void Process(string sources)
        {

            var delimiter = ConfigurationManager.AppSettings["Delimiter"];
            string sourcesParsingDelimiter = string.IsNullOrWhiteSpace(delimiter) ? "," : delimiter ; 

            List<string> soursesUrls = parser.Parse(sources, sourcesParsingDelimiter).ToList();

            Parallel.ForEach(soursesUrls, url =>
            {

                IDownloadStrategy downloadStrategy = downloadStrategyFactory.Build(url);

                downloadStrategy.Download(url);

            });

        }
    }
}
