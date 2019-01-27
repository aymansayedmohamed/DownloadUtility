using DownloadUtilityLogger;
using IBusiness.IHelpers;
using System;
using System.IO;

namespace Business.Helpers
{
    public class IOHelper : IIOHelper
    {
        private readonly ILogger logger;
        public IOHelper(ILogger logger)
        {
            this.logger = logger;
        }

        public string GetLocalDestinationPath(string remotePath, string localPath)
        {

            string tempFileDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localPath);

            logger.AddInformationLog(nameof(tempFileDirPath) + ": " + tempFileDirPath);

            DirectoryInfo tempFileDir = new DirectoryInfo(tempFileDirPath);

            logger.AddInformationLog(nameof(tempFileDir) + " object is created.");

            if (!tempFileDir.Exists)
            {
                logger.AddInformationLog(nameof(tempFileDir) + " not exists.");

                tempFileDir.Create();

                logger.AddInformationLog(nameof(tempFileDir) + " created.");
            }

            string fileName = remotePath.Substring(remotePath.LastIndexOf(@"/") + 1);

            string localDestinationPath = Path.Combine(tempFileDir.FullName, fileName);

            return BuildUniqueFilePath(localDestinationPath);
        }

        private string BuildUniqueFilePath(string path)
        {
            string dir = Path.GetDirectoryName(path);

            string fileName = Path.GetFileNameWithoutExtension(path);

            string fileExt = Path.GetExtension(path);

            string uniquePath = path;

            for (int i = 1; ; ++i)
            {
                if (!File.Exists(uniquePath))
                {
                    return uniquePath;
                }

                uniquePath = Path.Combine(dir, $"{fileName}_{i} { fileExt}");
            }

        }
    }
}
