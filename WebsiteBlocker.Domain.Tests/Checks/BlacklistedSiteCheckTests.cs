using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebsiteBlocker.Domain.Checks;

namespace WebsiteBlocker.Domain.Tests.Checks
{
    [TestClass]
    public class BlacklistedSiteCheckTests
    {
        #region - ShouldWebsiteBeBlocked -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullUrl()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>());

            //Act
            check.ValidateUrl(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullCheckList()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(null);

            //Act
            check.ValidateUrl("http://google.com");
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_SingleWord_WordDoesntMatch()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "word" });

            //Act
            var result = check.ValidateUrl("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_SingleWord_WordMatches()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "google" });

            //Act
            var result = check.ValidateUrl("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_SingleWord_WordMatches_DifferentCase()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "GOOGLE" });

            //Act
            var result = check.ValidateUrl("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_MultipleWords_Matches()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "blah", "google" });

            //Act
            var result = check.ValidateUrl("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}
