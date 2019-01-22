using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
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
           if(url == null || this.WhitelistedSites == null) throw new ArgumentNullException();

           return !this.WhitelistedSites.Any(x => IsSiteWhitelisted(url, x));
        }

        private bool IsSiteWhitelisted(string url, string site)
        {
            return Regex.Matches(url, site, RegexOptions.IgnoreCase).Count > 0;
        }
    }
}
