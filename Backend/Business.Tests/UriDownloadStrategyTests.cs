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
    public class UriDownloadStrategyTests
    {
        private IDownloadStrategy strategy;

        [TestInitialize]
        public void TestInitialize()
        {
            var logger = new Mock<ILogger>();
            var iOHelper = new Mock<IIOHelper>();

            strategy = new UriDownloadStrategy(logger.Object,iOHelper.Object);
        }


        [TestMethod]
        public void IsMatch_PassHttpProtocolUpperCase_returnTrue()
        {
            //arrange
            var protocol = "HTTP";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void IsMatch_PassHttpProtocolLowerCase_returnTrue()
        {
            //arrange
            var protocol = "http";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void IsMatch_PassHttpsProtocolUpperCase_returnTrue()
        {
            //arrange
            var protocol = "HTTPS";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void IsMatch_PassHttpsProtocolLowerCase_returnTrue()
        {
            //arrange
            var protocol = "https";

            //act
            var result = strategy.IsMatch(protocol);

            //assert
            Assert.AreEqual(result, true);
        }

    }
}
