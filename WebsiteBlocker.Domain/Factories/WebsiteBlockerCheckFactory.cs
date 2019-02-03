using System;
using System.Collections.Generic;
using WebsiteBlocker.Domain.Checks;
using WebsiteBlocker.Domain.Interfaces.Checks;
using WebsiteBlocker.Domain.Interfaces.Factories;

namespace WebsiteBlocker.Domain.Factories
{
    public class WebsiteBlockerCheckFactory : IWebsiteBlockerCheckFactory
    {
        public IList<IWebsiteBlockerCheck> CreateWebsiteBlockerChecks(IList<string> blacklistedSites,
                                                                      IList<string> whitelistedSites, 
                                                                      IList<string> blacklistedWords)
        {
            var result = new List<IWebsiteBlockerCheck>();

            if(blacklistedSites == null || whitelistedSites == null || blacklistedWords == null)
                throw new ArgumentNullException();

            result.Add(new BlacklistedSiteCheck(blacklistedSites));
            result.Add(new HtmlCheck(blacklistedWords));
            result.Add(new WhitelistedSiteCheck(whitelistedSites));

            return result;
        }
    }
}
