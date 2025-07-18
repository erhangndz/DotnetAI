using DotnetAI.WebUI.Services.SentimentAIServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class SentimentAIController(ISentimentAiService sentimentAiService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string text)
        {
            var response = await sentimentAiService.AnalyzeSentimentAsync(text);
            ViewBag.text = text;
            ViewBag.response = response;
            return View();
        }
    }
}
