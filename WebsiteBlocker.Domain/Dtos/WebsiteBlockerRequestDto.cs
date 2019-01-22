using System;
using System.Collections.Generic;
using System.Text;

namespace WebsiteBlocker.Domain.Dtos
{
    public class WebsiteBlockerRequestDto
    {
        public string Url { get; set; }
        public IList<string> BlacklistedSites { get; set; }
        public IList<string> WhitelistedSites { get; set; }
        public IList<string> BlacklistedWords { get; set; }
    }
}
