using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebsiteBlocker.Domain.Checks;

namespace WebsiteBlocker.Domain.Tests.Checks
{
    [TestClass]
    public class BlacklistedSiteCheckTests
    {
        #region - CheckWebsite -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CheckWebsite_NullUrl()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>());

            //Act
            check.CheckWebsite(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CheckWebsite_NullCheckList()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(null);

            //Act
            check.CheckWebsite("http://google.com");
        }

        [TestMethod]
        public void Test_CheckWebsite_SingleWord_WordDoesntMatch()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "word" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CheckWebsite_SingleWord_WordMatches()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "google" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CheckWebsite_SingleWord_WordMatches_DifferentCase()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "GOOGLE" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CheckWebsite_MultipleWords_Matches()
        {
            //Arrange
            var check = new BlacklistedSiteCheck(new List<string>() { "blah", "google" });

            //Act
            var result = check.CheckWebsite("http://google.com");

            //Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}
