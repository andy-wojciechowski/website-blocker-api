using System.Collections.Generic;

namespace WebsiteBlocker.API.Models
{
    public class WebsiteBlockerAppSettings
    {
        public IList<string> BlacklistedSites { get; set; }
        public IList<string> WhitelistedSites { get; set; }
        public IList<string> BlacklistedWords { get; set; }
    }
}
