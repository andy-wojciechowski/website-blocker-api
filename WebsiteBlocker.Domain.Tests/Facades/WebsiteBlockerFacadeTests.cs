using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteBlocker.Domain.Checks;
using WebsiteBlocker.Domain.Dtos;
using WebsiteBlocker.Domain.Facades;
using WebsiteBlocker.Domain.Interfaces;
using WebsiteBlocker.Domain.Interfaces.Factories;
using WebsiteBlocker.Domain.Interfaces.Services;

namespace WebsiteBlocker.Domain.Tests.Facades
{
    [TestClass]
    public class WebsiteBlockerFacadeTests
    {
        private IList<string> AssertBlacklistedSites { get; set; }
        private IList<string> AssertBlacklistedWords { get; set; }
        private IList<string> AssertWhitelistedSites { get; set; }
        private IWebsiteBlockerCheck[] AssertChecks { get; set; }

        #region - ShouldWebsiteBeBlocked -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullUrl()
        {
            //Arrange
            var facade = CreateWebsiteBlockerFacade();

            //Act
            facade.ShouldWebsiteBeBlocked(new WebsiteBlockerRequestDto() { Url = null});
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_True()
        {
            //Arrange
            var requestDto = CreateBasicRequestDto();

            var checks = CreateChecks(requestDto);

            var factory = CreateMockedFactory(checks);
            var service = CreateWebsiteBlockerService(requestDto.Url, true);

            var facade = CreateWebsiteBlockerFacade(service, factory);

            //Act
            var result = facade.ShouldWebsiteBeBlocked(requestDto);

            //Assert
            Assert.IsTrue(result);
            CollectionAssert.AreEquivalent(requestDto.BlacklistedSites.ToArray(), this.AssertBlacklistedSites.ToArray());
            CollectionAssert.AreEquivalent(requestDto.BlacklistedWords.ToArray(), this.AssertBlacklistedWords.ToArray());
            CollectionAssert.AreEquivalent(requestDto.WhitelistedSites.ToArray(), this.AssertWhitelistedSites.ToArray());
            CollectionAssert.AreEquivalent(checks.ToArray(), this.AssertChecks);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_False()
        {
            //Arrange
            var requestDto = CreateBasicRequestDto();

            var checks = CreateChecks(requestDto);

            var factory = CreateMockedFactory(checks);
            var service = CreateWebsiteBlockerService(requestDto.Url, false);
            var facade = CreateWebsiteBlockerFacade(service, factory);

            //Act
            var result = facade.ShouldWebsiteBeBlocked(requestDto);

            //Assert
            Assert.IsFalse(result);
            CollectionAssert.AreEquivalent(requestDto.BlacklistedSites.ToArray(), this.AssertBlacklistedSites.ToArray());
            CollectionAssert.AreEquivalent(requestDto.BlacklistedWords.ToArray(), this.AssertBlacklistedWords.ToArray());
            CollectionAssert.AreEquivalent(requestDto.WhitelistedSites.ToArray(), this.AssertWhitelistedSites.ToArray());
            CollectionAssert.AreEquivalent(checks.ToArray(), this.AssertChecks);
        }

        #endregion

        #region - Private Helpers -

        private WebsiteBlockerRequestDto CreateBasicRequestDto()
        {
            return new WebsiteBlockerRequestDto()
            {
                Url = "https://google.com",
                BlacklistedSites = new List<string>() { "site 0", "site 1", "site 2" },
                BlacklistedWords = new List<string>() { "blacklisted word 0", "blacklisted word 1" },
                WhitelistedSites = new List<string>() { "whitelisted word 0", "whitelisted word 1" }
            };
        }

        private WebsiteBlockerFacade CreateWebsiteBlockerFacade(IWebsiteBlockerService websiteBlockerService = null, IWebsiteBlockerCheckFactory websiteBlockerCheckFactory = null)
        {
            return new WebsiteBlockerFacade(websiteBlockerService, websiteBlockerCheckFactory);
        }

        private List<IWebsiteBlockerCheck> CreateChecks(WebsiteBlockerRequestDto requestDto)
        {
            var checks = new List<IWebsiteBlockerCheck>();
            checks.Add(new BlacklistedSiteCheck(requestDto.BlacklistedSites));
            checks.Add(new HtmlCheck(requestDto.BlacklistedWords));
            checks.Add(new WhitelistedSiteCheck(requestDto.WhitelistedSites));
            return checks;
        }

        private IWebsiteBlockerCheckFactory CreateMockedFactory(List<IWebsiteBlockerCheck> checks)
        {
            var factory = new Mock<IWebsiteBlockerCheckFactory>();
            factory.Setup(x => x.CreateWebsiteBlockerChecks(It.IsAny<IList<string>>(), It.IsAny<IList<string>>(), It.IsAny<IList<string>>())).Returns(checks).Callback((IList<string> blacklistedSites, IList<string> whitelistedSites, IList<string> blacklistedWords) =>
            {
                this.AssertBlacklistedSites = blacklistedSites;
                this.AssertWhitelistedSites = whitelistedSites;
                this.AssertBlacklistedWords = blacklistedWords;
            });
            return factory.Object;
        }

        private IWebsiteBlockerService CreateWebsiteBlockerService(string url, bool result)
        {
            var websiteBlockerService = new Mock<IWebsiteBlockerService>();
            websiteBlockerService.Setup(x => x.ShouldWebsiteBeBlocked(url, It.IsAny<IWebsiteBlockerCheck[]>())).Returns(result).Callback((string callbackUrl, IWebsiteBlockerCheck[] callbackChecks) =>
            {
                this.AssertChecks = callbackChecks;
            });
            return websiteBlockerService.Object;
        }
        #endregion
    }
}
