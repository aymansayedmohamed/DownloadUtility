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
    public class SFTPDownloadStrategyTests
    {
        private IDownloadStrategy strategy;

        [TestInitialize]
        public void TestInitialize()
        {
            var logger = new Mock<ILogger>();

            strategy = new SFTPDownloadStrategy(logger.Object);
        }


        [TestMethod]
        public void IsMatch_PassSFtpProtocolUpperCase_returnTrue()
        {
            //arrange
            var protocol = "SFTP";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void IsMatch_PassSFtpProtocolLowerCase_returnTrue()
        {
            //arrange
            var protocol = "sftp";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }

    }
}
