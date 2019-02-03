using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using WebsiteBlocker.Domain.Checks;
using HtmlAgilityPack;

namespace WebsiteBlocker.Domain.Tests.Checks
{
    [TestClass]
    public class HtmlCheckTests
    {
        #region - ShouldWebsiteBeBlocked -

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullUrl()
        {
            //Arrange
            var check = new HtmlCheck(new List<string>());

            //Act
            check.ShouldWebsiteBeBlocked(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_ShouldWebsiteBeBlocked_NullBlacklistedWords()
        {
            //Arrange
            var check = new HtmlCheck(null);

            //Act
            check.ShouldWebsiteBeBlocked("http://google.com");
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Test_ShouldWebsiteBeBlocked_NullHtmlStream()
        {
            //Arrange
            var check = new Mock<HtmlCheck>(new List<string>() { "blacklisted word" });

            check.Setup(x => x.ReadHtml("http://google.com")).Returns((Stream)null);

            //Act
            check.Object.ShouldWebsiteBeBlocked("http://google.com");
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_FalseResult_SingleNode()
        {
            //Arrange
            var check = new Mock<HtmlCheck>(new List<string>() { "blacklisted word" });
            var document = new HtmlDocument();
            CreateAndAppendTextNodes(document, "word1");

            check.Setup(x => x.ReadHtml("http://somewebsite.com")).Returns(Stream.Null);
            check.Setup(x => x.LoadHtmlDocument(It.IsAny<Stream>())).Returns(document);

            //Act
            var result = check.Object.ShouldWebsiteBeBlocked("http://somewebsite.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_TrueResult_SameCase_SingleNode()
        {
            var check = new Mock<HtmlCheck>(new List<string>() { "blacklisted word" });
            var document = new HtmlDocument();
            CreateAndAppendTextNodes(document, "blacklisted word");

            check.Setup(x => x.ReadHtml("http://somewebsite.com")).Returns(Stream.Null);
            check.Setup(x => x.LoadHtmlDocument(It.IsAny<Stream>())).Returns(document);

            //Act
            var result = check.Object.ShouldWebsiteBeBlocked("http://somewebsite.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_TrueResult_DifferentCase_SingleNode()
        {
            var check = new Mock<HtmlCheck>(new List<string>() { "blacklisted word" });
            var document = new HtmlDocument();
            CreateAndAppendTextNodes(document, "blAcklisted word");

            check.Setup(x => x.ReadHtml("http://somewebsite.com")).Returns(Stream.Null);
            check.Setup(x => x.LoadHtmlDocument(It.IsAny<Stream>())).Returns(document);

            //Act
            var result = check.Object.ShouldWebsiteBeBlocked("http://somewebsite.com");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_FalseResult_MultipleNodes()
        {
            var check = new Mock<HtmlCheck>(new List<string>() { "blacklisted word" });
            var document = new HtmlDocument();
            CreateAndAppendTextNodes(document, "word1", "word2", "word3");

            check.Setup(x => x.ReadHtml("http://somewebsite.com")).Returns(Stream.Null);
            check.Setup(x => x.LoadHtmlDocument(It.IsAny<Stream>())).Returns(document);

            //Act
            var result = check.Object.ShouldWebsiteBeBlocked("http://somewebsite.com");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_ShouldWebsiteBeBlocked_TrueResult_MultipleNodes()
        {
            var check = new Mock<HtmlCheck>(new List<string>() { "blacklisted word" });
            var document = new HtmlDocument();
            CreateAndAppendTextNodes(document, "word1", "BLACKLISTED WORD", "word3");

            check.Setup(x => x.ReadHtml("http://somewebsite.com")).Returns(Stream.Null);
            check.Setup(x => x.LoadHtmlDocument(It.IsAny<Stream>())).Returns(document);

            //Act
            var result = check.Object.ShouldWebsiteBeBlocked("http://somewebsite.com");

            //Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region - Private Helpers -

        private void CreateAndAppendTextNodes(HtmlDocument document, params string[] words)
        {
            foreach (var word in words)
            {
                var textNode = document.CreateTextNode(word);
                document.DocumentNode.AppendChild(textNode);
            }
        }

        #endregion
    }
}
