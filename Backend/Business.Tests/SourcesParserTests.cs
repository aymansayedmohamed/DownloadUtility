using System;
using System.Linq;
using DownloadUtilityLogger;
using IBusiness;
using IBusiness.IHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests
{
    //TODO Cover more test cases

    [TestClass]
    public class SourcesParserTests
    {
        private IParser parser;

        [TestInitialize]
        public void TestInitialize()
        {
            parser = new SourcesParser();
        }


        [TestMethod]
        public void Parse_TakeStringSeperatedComma_returnRighCount()
        {
            //arrange
            string sourses = "http://file,ftp://file";
            string delimiter = ",";
            int expectedCount = 2;
            //act
            var result = parser.Parse(sourses,delimiter);

            //assert
            Assert.AreEqual(result.Count(), expectedCount);
        }
     

    }
}
