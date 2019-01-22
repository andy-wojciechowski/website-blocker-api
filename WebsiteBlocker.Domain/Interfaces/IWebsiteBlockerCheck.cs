using System;
using System.Collections.Generic;
using System.Text;

namespace WebsiteBlocker.Domain.Interfaces
{
    public interface IWebsiteBlockerCheck
    {
        bool CheckWebsite(string url);
    }
}
