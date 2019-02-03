using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebsiteBlocker.Domain.Checks;

namespace WebsiteBlocker.Domain.Tests.Checks
{
    [TestClass]
    public class WhitelistedSiteCheckTests
    {
        #region - ShouldWebsiteBeBlocked -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullUrl()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>());

            //Act
            check.ShouldWebsiteBeBlocked(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullSites()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(null);

            //Act
            check.ShouldWebsiteBeBlocked("http://google.com");
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_SingleSite_DoesntMatch()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://website.com" });

            //Act
            var result = check.ShouldWebsiteBeBlocked("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_SingleSite_DoesMatch()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://google.com" });

            //Act
            var result = check.ShouldWebsiteBeBlocked("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_SingleWord_WordMatches_DifferentCase()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://GOOGLE.com" });

            //Act
            var result = check.ShouldWebsiteBeBlocked("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_MultipleWords_Matches()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://blah.com", "http://google.com" });

            //Act
            var result = check.ShouldWebsiteBeBlocked("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
