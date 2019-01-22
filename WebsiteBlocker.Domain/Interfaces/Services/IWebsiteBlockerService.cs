namespace WebsiteBlocker.Domain.Interfaces.Services
{
    public interface IWebsiteBlockerService
    {
        bool ShouldWebsiteBeBlocked(string url, params IWebsiteBlockerCheck[] checks);
    }
}
