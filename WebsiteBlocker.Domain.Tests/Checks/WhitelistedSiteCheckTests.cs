using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebsiteBlocker.Domain.Checks;

namespace WebsiteBlocker.Domain.Tests.Checks
{
    [TestClass]
    public class WhitelistedSiteCheckTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_CheckWebsite_NullUrl()
        {
            //Arrange
            var check = new WhitelistedSiteCheck(new List<string>());

            //Act
            check.CheckWebsite(null);
        }
    }
}
