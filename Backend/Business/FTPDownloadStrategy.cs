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

        public void Download(string source)
        {
            //Implement ftp download startgy

            //ftp://speedtest.tele2.net/5MB.zip;
            String Username = ""; 
            String Password = ""; 

            var localDestinationPath = DownloadFTPFile(source, Username, Password);

        }

        private string DownloadFTPFile(string remoteFtpPath, string username, string password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFtpPath);

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential(username, password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);

            string localDestinationPath = GetLocalDestinationPath(remoteFtpPath);

            using (FileStream writer = new FileStream(localDestinationPath, FileMode.Create))
            {

                long length = response.ContentLength;

                int bufferSize = 2048;

                int readCount;

                byte[] buffer = new byte[2048];

                readCount = responseStream.Read(buffer, 0, bufferSize);

                while (readCount > 0)
                {
                    writer.Write(buffer, 0, readCount);
                    readCount = responseStream.Read(buffer, 0, bufferSize);
                }
            }

            reader.Close();
            response.Close();

            return localDestinationPath;
        }

        private string GetLocalDestinationPath(string RemoteFtpPath)
        {
            string tempFileDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");

            logger.AddInformationLog(nameof(tempFileDirPath) + ": " + tempFileDirPath);

            DirectoryInfo tempFileDir = new DirectoryInfo(tempFileDirPath);

            logger.AddInformationLog(nameof(tempFileDir) + " object is created.");

            if (!tempFileDir.Exists)
            {
                logger.AddInformationLog(nameof(tempFileDir) + " not exists.");

                tempFileDir.Create();

                logger.AddInformationLog(nameof(tempFileDir) + " created.");
            }

            string fileName = RemoteFtpPath.Substring(RemoteFtpPath.LastIndexOf(@"/") + 1);

            string localDestinationPath =Path.Combine(tempFileDir.FullName,fileName);

            return localDestinationPath;
        }
    }
}
