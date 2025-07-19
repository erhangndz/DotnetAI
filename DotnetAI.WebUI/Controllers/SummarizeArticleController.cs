using DotnetAI.WebUI.Services.SummarizeArticleServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class SummarizeArticleController(ISummarizeArticleService summarizeArticleService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string text, string level)
        {
            var response = await summarizeArticleService.SummarizeTextAsync(text, level);
            ViewBag.response = response;
            ViewBag.text = text;
            ViewBag.level = level;
            return View();
        }
    }
}
