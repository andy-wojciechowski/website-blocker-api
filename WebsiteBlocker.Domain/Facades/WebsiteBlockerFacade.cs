using System;
using System.Linq;
using WebsiteBlocker.Domain.Dtos;
using WebsiteBlocker.Domain.Interfaces.Facades;
using WebsiteBlocker.Domain.Interfaces.Factories;
using WebsiteBlocker.Domain.Interfaces.Services;

namespace WebsiteBlocker.Domain.Facades
{
    public class WebsiteBlockerFacade : IWebsiteBlockerFacade
    {
        private IWebsiteBlockerService WebsiteBlockerService { get; }
        private IWebsiteBlockerCheckFactory WebsiteBlockerCheckFactory { get; }

        public WebsiteBlockerFacade(IWebsiteBlockerService websiteBlockerService, 
                                    IWebsiteBlockerCheckFactory websiteBlockerCheckFactory)
        {
            WebsiteBlockerService = websiteBlockerService;
            WebsiteBlockerCheckFactory = websiteBlockerCheckFactory;
        }

        public bool ShouldWebsiteBeBlocked(WebsiteBlockerRequestDto request)
        {
            if (request.Url == null) throw new ArgumentNullException();

            var checks = WebsiteBlockerCheckFactory.CreateWebsiteBlockerChecks(request.BlacklistedSites, request.WhitelistedSites, request.BlacklistedWords);

            return WebsiteBlockerService.ShouldWebsiteBeBlocked(request.Url, checks.ToArray());
        }
    }
}
