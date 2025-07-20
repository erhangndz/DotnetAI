using DotnetAI.WebUI.Services.WebScrapingServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAiWebScrapingController(IWebScrapingService webScrapingService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string url)
        {
            var response = await webScrapingService.ScrapeAndAnalyzeWebPageAsync(url);
            ViewBag.response = response;
            ViewBag.url = url;
            return View();
        }
    }
}
