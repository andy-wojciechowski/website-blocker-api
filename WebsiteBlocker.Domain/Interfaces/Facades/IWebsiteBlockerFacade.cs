using WebsiteBlocker.Domain.Dtos;

namespace WebsiteBlocker.Domain.Interfaces.Facades
{
    public interface IWebsiteBlockerFacade
    {
        bool ShouldWebsiteBeBlocked(WebsiteBlockerRequestDto request);
    }
}
