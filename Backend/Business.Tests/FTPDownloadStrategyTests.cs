using System;
using DownloadUtilityLogger;
using IBusiness;
using IBusiness.IHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests
{
    //TODO Cover more test cases

    [TestClass]
    public class FTPDownloadStrategyTests
    {
        private IDownloadStrategy strategy;

        [TestInitialize]
        public void TestInitialize()
        {
            var logger = new Mock<ILogger>();
            var iOHelper = new Mock<IIOHelper>();

            strategy = new FTPDownloadStrategy(logger.Object,iOHelper.Object);
        }


        [TestMethod]
        public void IsMatch_PassFtpProtocolUpperCase_returnTrue()
        {
            //arrange
            var protocol = "FTP";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void IsMatch_PassFtpProtocolLowerCase_returnTrue()
        {
            //arrange
            var protocol = "ftp";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }

    }
}
