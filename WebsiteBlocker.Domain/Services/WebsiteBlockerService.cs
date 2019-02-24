using System;
using System.Linq;
using WebsiteBlocker.Domain.Checks;
using WebsiteBlocker.Domain.Interfaces.Checks;
using WebsiteBlocker.Domain.Interfaces.Services;

namespace WebsiteBlocker.Domain.Services
{
    public class WebsiteBlockerService : IWebsiteBlockerService
    {
        public bool ShouldWebsiteBeBlocked(string url, params IWebsiteBlockerCheck[] checks)
        {
            if (url == null || checks.Any(x => x is null)) throw new ArgumentNullException();

            var result = GetWhitelistedCheckResult(url, checks);
            return result ? !result : checks.Any(x => x.ShouldWebsiteBeBlocked(url));
        }

        private bool GetWhitelistedCheckResult(string url, IWebsiteBlockerCheck[] checks)
        {
            var result = false;
            var whitelistedSiteCheck = checks.SingleOrDefault(x => x is WhitelistedSiteCheck);
            if (whitelistedSiteCheck != null) result = whitelistedSiteCheck.ShouldWebsiteBeBlocked(url);
            return result;
        }
    }
}
