using System;
using System.Collections.Generic;
using System.Linq;
using DownloadUtilityLogger;
using IBusiness;
using IBusiness.IHelpers;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests
{
    //TODO Cover more test cases

    [TestClass]
    public class DownloadManagerTests
    {
        private IDownloadManager downloadManager;

        private string httpUrl = "https://www.everyarabstudent.com/img2017/top_more_retina.png";
        private string localPath = "img2017/top_more_retina.png";
        private Mock<IDownloadStrategyFactory> httpDownloadStrategyFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            var delimator = ",";

            List<string> soursesUrls = new List<string>()
            {
                httpUrl
            };

            var logger = new Mock<ILogger>();
            var iOHelper = new Mock<IIOHelper>();

            var parser = new Mock<IParser>();
            parser.Setup(x => x.Parse(httpUrl, delimator)).Returns(soursesUrls.AsQueryable());

             httpDownloadStrategyFactory = new Mock<IDownloadStrategyFactory>() ;

            httpDownloadStrategyFactory.Setup(x => x.Build(httpUrl)).Returns(new UriDownloadStrategy(logger.Object,iOHelper.Object))
                .Callback<IDownloadStrategy>(o => o.Download(httpUrl, localPath));

            var repoDownloadedFile = new Mock<IRepository<DomainModels.DownloadedFile>>();

            var repoProcessingStatus = new Mock<IRepository<DomainModels.ProcessingStatu>>();

            downloadManager = new DownloadManager(parser.Object, httpDownloadStrategyFactory.Object,repoDownloadedFile.Object,repoProcessingStatus.Object,logger.Object,iOHelper.Object);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_PassEmptyResourcesString_ThrowArgumentException()
        {
            //arrange
            var resources = "";

            //act
            downloadManager.Process(resources);

        }

        [TestMethod]
        public void Process_PassValidSources_WorkCorrectly()
        {
            //arrange
            var sources = httpUrl;

            //act
            downloadManager.Process(sources);

        }

    }
}
