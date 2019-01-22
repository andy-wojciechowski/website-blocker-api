using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebsiteBlocker.Domain.Checks;

namespace WebsiteBlocker.Domain.Tests.Checks
{
    [TestClass]
    public class WhitelistedSiteCheckTests
    {
        #region - CheckWebsite -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CheckWebsite_NullUrl()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>());

            //Act
            check.CheckWebsite(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CheckWebsite_NullSites()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(null);

            //Act
            check.CheckWebsite("http://google.com");
        }

        [TestMethod]
        public void Test_CheckWebsite_SingleSite_DoesntMatch()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://website.com" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CheckWebsite_SingleSite_DoesMatch()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://google.com" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CheckWebsite_SingleWord_WordMatches_DifferentCase()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://GOOGLE.com" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CheckWebsite_MultipleWords_Matches()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>() { "http://blah.com", "http://google.com" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
