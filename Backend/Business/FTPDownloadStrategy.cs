using DownloadUtilityLogger;
using IBusiness;
using IBusiness.IHelpers;
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
        private readonly IIOHelper iOHelper;

        private readonly string protocol = "FTP";

        public FTPDownloadStrategy(ILogger logger, IIOHelper iOHelper)
        {
            this.logger = logger;
            this.iOHelper = iOHelper;
        }

        public bool IsMatch(string protocol)
        {
            return  protocol.ToUpper() == this.protocol;
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
                localDestinationPath = iOHelper.GetLocalDestinationPath(remoteFtpPath, localPath);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFtpPath);

                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(username, password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                var bufferSizeConfigValue = ConfigurationManager.AppSettings["BufferSize"];
                logger.AddInformationLog($"BufferSize config value: {bufferSizeConfigValue}");

                var bufferSize = !string.IsNullOrWhiteSpace(bufferSizeConfigValue) ? Convert.ToInt32(bufferSizeConfigValue) : 2048;

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

                logger.AddInformationLog($"Download file {remoteFtpPath} completed");

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

    }
}
