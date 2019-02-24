using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WebsiteBlocker.Domain.Checks;
using WebsiteBlocker.Domain.Interfaces.Checks;
using WebsiteBlocker.Domain.Services;

namespace WebsiteBlocker.Domain.Tests.Services
{
    [TestClass]
    public class WebsiteBlockerServiceTests
    {
        #region - ShouldWebsiteBeBlocked -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullUrl()
        {
            //Arrange
            var service = new WebsiteBlockerService();

            //Act
            service.ShouldWebsiteBeBlocked(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullCheck()
        {
            //Arrange
            var service = new WebsiteBlockerService();

            //Act
            service.ShouldWebsiteBeBlocked("https://google.com", null);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_AllTrue()
        {
            //Arrange
            var url = "https://google.com";
            var service = new WebsiteBlockerService();

            List<IWebsiteBlockerCheck> checks = CreatedMockedWebsiteChecks(url, true, 3);

            //Act
            var result = service.ShouldWebsiteBeBlocked(url, checks.ToArray());

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_AllFalse()
        {
            //Arrange
            var url = "https://google.com";
            var service = new WebsiteBlockerService();

            var checks = CreatedMockedWebsiteChecks(url, false, 3);

            //Act
            var result = service.ShouldWebsiteBeBlocked(url, checks.ToArray());

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_OneTrue()
        {
            //Arrange
            var url = "https://google.com";
            var service = new WebsiteBlockerService();

            var checks = CreatedMockedWebsiteChecks(url, false, 2);
            checks.Add(CreateMockedCheck(url, true));

            //Act
            var result = service.ShouldWebsiteBeBlocked(url, checks.ToArray());

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_NotWhitelisted_ShouldNotBlock()
        {
            //Arrange
            var url = "https://google.com";
            var service = new WebsiteBlockerService();

            var checks = new List<IWebsiteBlockerCheck>();

            var whitelistedCheck = new Mock<WhitelistedSiteCheck>(new List<string>());
            whitelistedCheck.Setup(x => x.ShouldWebsiteBeBlocked(url)).Returns(true);
            checks.Add(whitelistedCheck.Object);

            CreateMockedCheck(url, false);

            //Act
            var result = service.ShouldWebsiteBeBlocked(url, checks.ToArray());

            //Assert
            Assert.IsFalse(result);
        }

        #endregion


        #region - Private Helpers -

        private List<IWebsiteBlockerCheck> CreatedMockedWebsiteChecks(string url, bool checkResult, int instancesToCreate)
        {
            var checks = new List<IWebsiteBlockerCheck>();
            for (var i = 0; i < instancesToCreate; i++)
            {
                var mockedCheck = CreateMockedCheck(url, checkResult);
                checks.Add(mockedCheck);
            }

            return checks;
        }

        private IWebsiteBlockerCheck CreateMockedCheck(string url, bool checkResult)
        {
            var mockedCheck = new Mock<IWebsiteBlockerCheck>();
            mockedCheck.Setup(x => x.ShouldWebsiteBeBlocked(url)).Returns(checkResult);
            return mockedCheck.Object;
        }

        #endregion
    }
}
