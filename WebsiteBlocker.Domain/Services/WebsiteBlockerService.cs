using System;
using System.Linq;
using WebsiteBlocker.Domain.Interfaces;
using WebsiteBlocker.Domain.Interfaces.Services;

namespace WebsiteBlocker.Domain.Services
{
    public class WebsiteBlockerService : IWebsiteBlockerService
    {
        public bool ShouldWebsiteBeBlocked(string url, params IWebsiteBlockerCheck[] checks)
        {
            if(url == null || checks.Any(x => x is null)) throw new ArgumentNullException();

            return checks.Any(x => x.CheckWebsite(url));
        }
    }
}
