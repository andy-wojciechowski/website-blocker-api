namespace WebsiteBlocker.Domain.Interfaces.Checks
{
    public interface IWebsiteBlockerCheck
    {
        bool ShouldWebsiteBeBlocked(string url);
    }
}
