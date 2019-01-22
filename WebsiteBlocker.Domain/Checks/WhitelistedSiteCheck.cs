using System;
using System.Collections.Generic;
using System.Text;
using WebsiteBlocker.Domain.Interfaces;

namespace WebsiteBlocker.Domain.Checks
{
    public class WhitelistedSiteCheck : IWebsiteBlockerCheck
    {
        private IList<string> WhitelistedSites { get; }

        public WhitelistedSiteCheck(IList<string> whitelistedSites)
        {
            WhitelistedSites = whitelistedSites;
        }

        public bool CheckWebsite(string url)
        {
            throw new ArgumentNullException();
        }
    }
}
