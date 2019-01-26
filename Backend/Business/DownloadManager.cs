using DomainModels;
using DownloadUtilityLogger;
using IBusiness;
using IDataAccess;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using IViewModels;
namespace Business
{
    public class DownloadManager : IDownloadManager
    {
        private readonly IParser parser;
        private readonly IDownloadStrategyFactory downloadStrategyFactory;
        private IRepository<DomainModels.DownloadedFile> repoDownloadedFile; //to do use interface
        private IRepository<DomainModels.ProcessingStatu> repoProcessingStatus; //to do use interface
        private readonly ILogger logger;


        public DownloadManager(IParser parser, IDownloadStrategyFactory downloadStrategyFactory, IRepository<DomainModels.DownloadedFile> repoDownloadedFile
             , IRepository<DomainModels.ProcessingStatu> repoProcessingStatus, ILogger logger)
        {
            this.parser = parser;
            this.downloadStrategyFactory = downloadStrategyFactory;
            this.repoDownloadedFile = repoDownloadedFile;
            this.repoProcessingStatus = repoProcessingStatus;
            this.logger = logger;

        }

        public void Process(string sources)
        {
            logger.AddInformationLog($"sources value: {sources}");

            string delimiter = ConfigurationManager.AppSettings["Delimiter"];
            logger.AddInformationLog($"Delimiter config value: {delimiter}");

            string sourcesParsingDelimiter = string.IsNullOrWhiteSpace(delimiter) ? "," : delimiter;
            logger.AddInformationLog($"Sources parsing delimiter: {sourcesParsingDelimiter}");

            string localPath = ConfigurationManager.AppSettings["LocalDirectory"];
            logger.AddInformationLog($"LocalDirectory config value: {localPath}");

            List<string> soursesUrls = parser.Parse(sources, sourcesParsingDelimiter).ToList();
            logger.AddInformationLog($"sourses Urls counts after parsing: {soursesUrls?.Count}");

            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();

            using (repoDownloadedFile)
            {
                Parallel.ForEach(soursesUrls, url =>
                {
                    try
                    {
                        IDownloadStrategy downloadStrategy = downloadStrategyFactory.Build(url);

                        string localDestinationPath = downloadStrategy.Download(url, localPath);
                        logger.AddInformationLog($"Local path is: {localPath} for URL: {url} ");

                        repoDownloadedFile.Add(new DomainModels.DownloadedFile()
                        {
                            FileRemotePath = url,
                            LocalPath = localDestinationPath,
                            ProcessingStatus = (int)DownloadStatutes.ReadyForProcessing,
                        });

                        repoDownloadedFile.SaveChanges();

                        logger.AddInformationLog($"Source :{url} saved to the database with status ready for processing");
                    }
                    catch (Exception ex)
                    {
                        exceptions.Enqueue(ex);
                    }
                });

                if (exceptions.Any())
                {
                    throw new AggregateException(exceptions);
                }
            }

        }

        public IQueryable<IDownloadedFile> GetReadyForProcessingFiles()
        {

            return
                (from file in repoDownloadedFile.GetAll()
                 join status in repoProcessingStatus.GetAll()
                 on file.ProcessingStatus equals status.Id
                 where file.ProcessingStatus == (int)DownloadStatutes.ReadyForProcessing
                 select new ViewModels.DownloadedFile()
                 {
                     Id = file.Id,
                     Source = file.FileRemotePath,
                     Url = file.LocalPath,
                     ProcessingStatusId = file.ProcessingStatus,
                     ProcessingStatus = status.Status
                 }).AsQueryable();
        }



        public IDownloadedFile RejectFile(IDownloadedFile file)
        {
            return UpdateStatus(file,DownloadStatutes.Rejected);
        }
        public IDownloadedFile ApproveFile(IDownloadedFile file)
        {
            return UpdateStatus(file, DownloadStatutes.Approved);
        }

        private IDownloadedFile UpdateStatus(IDownloadedFile file, DownloadStatutes statuts)
        {
            using (repoDownloadedFile)
            {
                var domainFile = repoDownloadedFile.Find(file.Id);

                domainFile.ProcessingStatus = (int)statuts;

                repoDownloadedFile.SaveChanges();

                return new ViewModels.DownloadedFile()
                {
                    Id = file.Id,
                    ProcessingStatus = statuts.ToString(),
                    Source = file.Source,
                    ProcessingStatusId = domainFile.ProcessingStatus,
                    Url = file.Url
                };
            }
        }

    }
}
