using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebsiteBlocker.Domain.Checks;
using WebsiteBlocker.Domain.Factories;

namespace WebsiteBlocker.Domain.Tests.Factories
{
    [TestClass]
    public class WebsiteBlockerCheckFactoryTests
    {
        #region - CreateWebsiteBlockerChecks -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CreateWebsiteBlockerChecks_NullBlacklistedSites()
        {
            //Arrange
            var factory = new WebsiteBlockerCheckFactory();

            //Act
            factory.CreateWebsiteBlockerChecks(null, new List<string>(), new List<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CreateWebsiteBlockerChecks_NullWhitelistedSites()
        {
            //Arrange
            var factory = new WebsiteBlockerCheckFactory();

            //Act
            factory.CreateWebsiteBlockerChecks(new List<string>(), null, new List<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CreateWebsiteBlockerChecks_NullBlacklistedWords()
        {
            //Arrange
            var factory = new WebsiteBlockerCheckFactory();

            //Act
            factory.CreateWebsiteBlockerChecks(new List<string>(), new List<string>(), null);
        }

        [TestMethod]
        public void Test_CreateWebsiteBlockerChecks()
        {
            //Arrange
            var factory = new WebsiteBlockerCheckFactory();

            //Act
            var result = factory.CreateWebsiteBlockerChecks(new List<string>(), new List<string>(), new List<string>());

            //Assert
            Assert.AreEqual(3, result.Count);

            Assert.IsInstanceOfType(result[0], typeof(BlacklistedSiteCheck));
            Assert.IsInstanceOfType(result[1], typeof(HtmlCheck));
            Assert.IsInstanceOfType(result[2], typeof(WhitelistedSiteCheck));
        }

        #endregion
    }
}
