using System;
using System.Collections.Generic;
using System.Text;
using WebsiteBlocker.Domain.Interfaces;

namespace WebsiteBlocker.Domain.Checks
{
    public class HtmlCheck : IWebsiteBlockerCheck
    {
        private IList<string> BlacklistedWords { get; }

        public HtmlCheck(IList<string> blacklistedWords)
        {
            BlacklistedWords = blacklistedWords;
        }

        public bool CheckWebsite(string url)
        {
            throw new NotImplementedException();
        }
    }
}
