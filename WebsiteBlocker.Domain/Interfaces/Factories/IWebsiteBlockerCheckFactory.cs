using System;
using System.Collections.Generic;
using System.Text;

namespace WebsiteBlocker.Domain.Interfaces.Factories
{
    public interface IWebsiteBlockerCheckFactory
    {
        IList<IWebsiteBlockerCheck> CreateWebsiteBlockerChecks(IList<string> blacklistedSites, 
                                                               IList<string> whitelistedSites, 
                                                               IList<string> blacklistedWords);
    }
}
