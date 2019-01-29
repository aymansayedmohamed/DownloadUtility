using IBusiness;
using Renci.SshNet;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DownloadUtilityLogger;
using System.IO;

namespace Business
{
    public class SFTPDownloadStrategy : IDownloadStrategy
    {
        private readonly string protocol = "SFTP";
        private readonly ILogger logger;

        public SFTPDownloadStrategy(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsMatch(string protocol)
        {
            return protocol.ToUpper() == this.protocol; 
        }

        public string Download(string source, string localPath)
        {
            //Implement ftp download startgy    

            string host = "";
            int port = 22;
            string username = ""; // get it from database confuguratuins
            string password = "";  // get it from database confuguratuins
            string sftpInboundPath = ""; // get it from database confuguratuins
            string filename = "";

           return DownloadSFTPFile(host, port, username, password, sftpInboundPath, filename);
        }

        private string DownloadSFTPFile(string host, int port, string username, string password, string sftpInboundPath, string filename)
        {
            try
            {
                using (SftpClient client = new SftpClient(host, port, username, password))
                {
                    logger.AddInformationLog(nameof(client) + " is created.");

                    client.Connect();

                    logger.AddInformationLog(nameof(client) + " is connected.");

                    if (client.IsConnected)
                    {
                        logger.AddInformationLog("FTP Connection established.");

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

                        string tempFilePath = $"{tempFileDir.FullName}\\{filename}";

                        logger.AddInformationLog(nameof(tempFilePath) + ": " + tempFilePath);

                        // Delete the file if exists.
                        if (File.Exists(tempFilePath))
                        {
                            logger.AddInformationLog(nameof(tempFilePath) + " exists.");

                            File.Delete(tempFilePath);

                            logger.AddInformationLog(nameof(tempFilePath) + " deleted.");
                        }

                        using (FileStream file = File.OpenWrite(tempFilePath))
                        {
                            logger.AddInformationLog(nameof(file) + " opened.");

                            string bufferSize = ConfigurationManager.AppSettings["BufferSize"];//4096

                            client.BufferSize = string.IsNullOrWhiteSpace(bufferSize) ? 4096 : Convert.ToUInt32(bufferSize);
                            logger.AddInformationLog($"BufferSize config value: {client?.BufferSize}");

                            client.DownloadFile($"{sftpInboundPath}{filename}", file);

                            logger.AddInformationLog($"file download from sftp success at path : {tempFilePath}");
                        }

                        return tempFilePath;

                    }
                    else
                    {
                        logger.AddErrorLog("Unable to establish FTP Connection.");
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.AddErrorLog("Error downloading the file via FTP.", ex);

                throw;
            }
        }

    }
}
