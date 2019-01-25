using DownloadUtilityLogger;
using IBusiness;
using Renci.SshNet;
using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace Business
{
    public class FTPDownloadStrategy : IDownloadStrategy
    {
        private readonly ILogger logger;
        private readonly string protocol = "FTP";

        public FTPDownloadStrategy(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsMatch(string protocol)
        {
            return protocol == this.protocol;
        }

        public string Download(string source, string localPath)
        {
            //ftp://speedtest.tele2.net/5MB.zip
            String username = ""; 
            String password = ""; 

            return DownloadFTPFile(source, localPath, username, password);

        }

        private string DownloadFTPFile(string remoteFtpPath, string localPath, string username, string password)
        {
            string localDestinationPath = string.Empty;
            try
            {
                localDestinationPath = GetLocalDestinationPath(remoteFtpPath, localPath);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFtpPath);

                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(username, password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(responseStream);

                var bufferSizeConfigValue = ConfigurationManager.AppSettings["BufferSize"];
                logger.AddInformationLog($"BufferSize config value: {bufferSizeConfigValue}");

                var bufferSize = !string.IsNullOrWhiteSpace(bufferSizeConfigValue) ? Convert.ToInt32(bufferSizeConfigValue) : 2048;

                using (FileStream writer = new FileStream(localDestinationPath, FileMode.Create))
                {
                    long length = response.ContentLength;

                    byte[] buffer = new byte[bufferSize];

                    int readCount = responseStream.Read(buffer, 0, bufferSize);

                    while (readCount > 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }
                }

                logger.AddInformationLog($"Download file {remoteFtpPath} completed");

                reader.Close();
                response.Close();

                return localDestinationPath;
            }
            catch(Exception ex)
            {
                var fileInfo = new FileInfo(localDestinationPath);

                if (fileInfo.Exists)
                {
                    File.Delete(fileInfo.FullName);
                }

                logger.AddInformationLog($"Download file {remoteFtpPath} didn't complete");
                throw;
            }
        }

        private string GetLocalDestinationPath(string remoteFtpPath, string localPath)
        {
            string tempFileDirPath = localPath;

            if(string.IsNullOrEmpty(tempFileDirPath))
            {
                tempFileDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
            }
                
            logger.AddInformationLog(nameof(tempFileDirPath) + ": " + tempFileDirPath);

            DirectoryInfo tempFileDir = new DirectoryInfo(tempFileDirPath);

            logger.AddInformationLog(nameof(tempFileDir) + " object is created.");

            if (!tempFileDir.Exists)
            {
                logger.AddInformationLog(nameof(tempFileDir) + " not exists.");

                tempFileDir.Create();

                logger.AddInformationLog(nameof(tempFileDir) + " created.");
            }

            string fileName = remoteFtpPath.Substring(remoteFtpPath.LastIndexOf(@"/") + 1);

            string localDestinationPath =Path.Combine(tempFileDir.FullName,fileName);

            return localDestinationPath;
        }
    }
}
