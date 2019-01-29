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
using System.IO;
using static ViewModels.Constants;
using IBusiness.IHelpers;

namespace Business
{
    public class DownloadManager : IDownloadManager
    {
        private readonly IParser parser;
        private readonly IDownloadStrategyFactory downloadStrategyFactory;
        private IRepository<DomainModels.DownloadedFile> repoDownloadedFile; //to do use interface
        private IRepository<DomainModels.ProcessingStatu> repoProcessingStatus; //to do use interface
        private readonly ILogger logger;
        private readonly IIOHelper iOHelper;



        public DownloadManager(IParser parser, IDownloadStrategyFactory downloadStrategyFactory, IRepository<DomainModels.DownloadedFile> repoDownloadedFile
             , IRepository<DomainModels.ProcessingStatu> repoProcessingStatus, ILogger logger, IIOHelper iOHelper)
        {
            this.parser = parser;
            this.downloadStrategyFactory = downloadStrategyFactory;
            this.repoDownloadedFile = repoDownloadedFile;
            this.repoProcessingStatus = repoProcessingStatus;
            this.logger = logger;
            this.iOHelper = iOHelper;

        }

        public void Process(string sources)
        {
            //TODO : Move this logic to separate windows service and make the service listen on Message Queue like Rabbitmq 
            
            logger.AddInformationLog($"sources value: {sources}");

            //Data validations
            if (string.IsNullOrWhiteSpace(sources))
                throw new ArgumentException($"The sources Is null or empty string.");

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

                            string physicalPath = downloadStrategy.Download(url, localPath);
                            string virtualPath = $"{localPath.Replace('\\', '/')}/{Path.GetFileName(physicalPath)}";
                            logger.AddInformationLog($"Local path is: {localPath} for URL: {url} ");

                            repoDownloadedFile.Add(new DomainModels.DownloadedFile()
                            {
                                FileRemotePath = url,
                                LocalPath = virtualPath,
                                ProcessingStatus = (int)DownloadStatutes.ReadyForProcessing,
                            });

                            repoDownloadedFile.SaveChanges();

                            logger.AddInformationLog($"Source :{url} saved to the database with status ready for processing");
                        }
                        catch (Exception ex)
                        {
                             exceptions.Enqueue(ex);
                             logger.AddErrorLog(ex);
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
            var hostUrl = ConfigurationManager.AppSettings["HostUrl"];
            if (!hostUrl.EndsWith("/"))
                hostUrl += "/";

            return
                (from file in repoDownloadedFile.GetAll()
                 join status in repoProcessingStatus.GetAll()
                 on file.ProcessingStatus equals status.Id
                 where file.ProcessingStatus == (int)DownloadStatutes.ReadyForProcessing
                 select new ViewModels.DownloadedFile()
                 {
                     Id = file.Id,
                     Source = file.FileRemotePath,
                     Url = hostUrl + file.LocalPath,
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

        public void GetFilesType(List<IDownloadedFile> files)
        {
            foreach (var file in files)
            {
                var extention = Path.GetExtension(file.Url)?.ToString().Replace(".", "");
                file.FileType = iOHelper.GetFileType(extention).ToString();
            }
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
