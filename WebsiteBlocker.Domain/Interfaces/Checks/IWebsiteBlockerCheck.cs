namespace WebsiteBlocker.Domain.Interfaces.Checks
{
    public interface IWebsiteBlockerCheck
    {
        bool ValidateUrl(string url);
    }
}
