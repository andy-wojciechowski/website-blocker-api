using Microsoft.AspNetCore.Mvc;
using System;
using WebsiteBlocker.Domain.Interfaces.Facades;
using WebsiteBlocker.Domain.Dtos;
using WebsiteBlocker.API.Models;

namespace WebsiteBlocker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        private IWebsiteBlockerFacade WebsiteBlockerFacade { get; }
        private WebsiteBlockerAppSettings AppSettings { get; }

        public HomeController(IWebsiteBlockerFacade websiteBlockerFacade, WebsiteBlockerAppSettings appSettings)
        {
            WebsiteBlockerFacade = websiteBlockerFacade;
            AppSettings = appSettings;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        public IActionResult CheckWebsite(string url)
        {
            if(url == null || !IsValidUrl(url))
                return BadRequest();

            var result = WebsiteBlockerFacade.ShouldWebsiteBeBlocked(new WebsiteBlockerRequestDto() {
                Url = url,
                BlacklistedSites = AppSettings.BlacklistedSites,
                BlacklistedWords = AppSettings.BlacklistedWords,
                WhitelistedSites = AppSettings.WhitelistedSites
            });

            return Ok(result);
        }

        private bool IsValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
