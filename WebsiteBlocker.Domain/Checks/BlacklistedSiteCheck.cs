using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebsiteBlocker.Domain.Interfaces.Checks;

namespace WebsiteBlocker.Domain.Checks
{
    public class BlacklistedSiteCheck : IWebsiteBlockerCheck
    {
        private IList<string> BlacklistedSites { get; }

        public BlacklistedSiteCheck(IList<string> blacklistedSites)
        {
            this.BlacklistedSites = blacklistedSites;
        }

        public virtual bool ValidateUrl(string url)
        {
            if (url == null || this.BlacklistedSites == null) throw new ArgumentNullException();

            return this.BlacklistedSites.Any(x => DoesUrlContainWord(url, x));
        }

        private bool DoesUrlContainWord(string url, string blacklistedWord)
        {
            return Regex.Matches(url, blacklistedWord, RegexOptions.IgnoreCase).Count > 0;
        }
    }
}
