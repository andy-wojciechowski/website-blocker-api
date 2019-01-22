using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebsiteBlocker.API.Controllers;
using Moq;
using WebsiteBlocker.Domain.Interfaces.Facades;
using WebsiteBlocker.Domain.Dtos;

namespace WebsiteBlocker.API.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        #region - CheckWebsite -

        [TestMethod]
        public void Test_CheckWebsite_NoUrl()
        {
            //Arrange
            var controller = CreateHomeController();

            //Act
            var result = controller.CheckWebsite(null);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Test_CheckWebsite_InvalidUrl()
        {
            //Arrange
            var controller = CreateHomeController();

            //Act
            var result = controller.CheckWebsite("invalidurl");

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Test_CheckWebsite_OnlyHttpUrls()
        {
            //Arrange
            var controller = CreateHomeController();

            //Act
            var result = controller.CheckWebsite("C:\\temp");

            //ASsert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Test_CheckWebsite_DontBlockWebsite()
        {
            //Arrange
            var url = "http://google.com";

            var websiteBlockerFacade = MockWebsiteBlockerFacade(url, false);
            var controller = CreateHomeController(websiteBlockerFacade);

            //Act
            var result = controller.CheckWebsite(url);

            //Assert
            AssertOkResult(false, result);
        }

        [TestMethod]
        public void Test_CheckWebsite_BlockWebsite()
        {
            //Arrange
            var url = "https://google.com";

            var websiteBlockerFacade = MockWebsiteBlockerFacade(url, true);
            var controller = CreateHomeController(websiteBlockerFacade);

            //Act
            var result = controller.CheckWebsite(url);

            //Assert
            AssertOkResult(true, result);
        }

        #endregion

        #region - Private Helpers -

        private void AssertOkResult(bool expectedResult, IActionResult actual)
        {
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));

            var okResult = (OkObjectResult)actual;
            Assert.IsInstanceOfType(okResult.Value, typeof(bool));
            Assert.AreEqual(expectedResult, (bool)okResult.Value);
        }

        private HomeController CreateHomeController(IWebsiteBlockerFacade websiteBlockerFacade = null)
        {
            return new HomeController(websiteBlockerFacade);
        }

        private IWebsiteBlockerFacade MockWebsiteBlockerFacade(string url, bool websiteBlockedValue)
        {
            var websiteBlockerFacade = new Mock<IWebsiteBlockerFacade>();
            websiteBlockerFacade.Setup(x => x.ShouldWebsiteBeBlocked(It.IsAny<WebsiteBlockerRequestDto>())).Returns(websiteBlockedValue);
            return websiteBlockerFacade.Object;
        }

        #endregion
    }
}
