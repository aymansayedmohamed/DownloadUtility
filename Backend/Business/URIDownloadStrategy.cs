using DownloadUtilityLogger;
using IBusiness;
using IBusiness.IHelpers;
using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace Business
{
    public class UriDownloadStrategy : IDownloadStrategy
    {
        private readonly ILogger logger;
        private readonly IIOHelper iOHelper;

        private readonly string httpProtocol = "HTTP";
        private readonly string httpsProtocol = "HTTPS";

        public UriDownloadStrategy(ILogger logger, IIOHelper iOHelper)
        {
            this.logger = logger;
            this.iOHelper = iOHelper;
        }

        public bool IsMatch(string protocol)
        {
            return (protocol == httpProtocol || protocol == httpsProtocol);
        }

        public string Download(string source, string localPath)
        {
            return DownloadUriFile(source, localPath);

        }

        private string DownloadUriFile(string remotePath, string localPath)
        {
            string localDestinationPath = string.Empty;

            try
            {
                localDestinationPath = iOHelper.GetLocalDestinationPath(remotePath, localPath);

                HttpWebRequest httpRequest = (HttpWebRequest)

                WebRequest.Create(remotePath);

                httpRequest.Method = WebRequestMethods.Http.Get;

                HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();

                Stream responseStream = response.GetResponseStream();

                string bufferSizeConfigValue = ConfigurationManager.AppSettings["BufferSize"];
                logger.AddInformationLog($"BufferSize config value: {bufferSizeConfigValue}");

                int bufferSize = !string.IsNullOrWhiteSpace(bufferSizeConfigValue) ? Convert.ToInt32(bufferSizeConfigValue) : 2048;

                using (FileStream writer = new FileStream(localDestinationPath, FileMode.Create))
                {
                    byte[] buffer = new byte[bufferSize];

                    int readCount = responseStream.Read(buffer, 0, bufferSize);

                    while (readCount > 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        readCount = responseStream.Read(buffer, 0, bufferSize);

                    }
                }

                logger.AddInformationLog($"Download file {remotePath} completed");

                response.Close();

                return localDestinationPath;
            }
            catch (Exception)
            {
                FileInfo fileInfo = new FileInfo(localDestinationPath);

                if (fileInfo.Exists)
                {
                    File.Delete(fileInfo.FullName);
                }

                logger.AddInformationLog($"Download file {remotePath} didn't complete");
                throw;
            }
        }

       
    }
}
