using DotnetAI.WebUI.Services.OpenAITranslateServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAITranslateController(IOpenAiTranslateService openAiTranslateService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string text)
        {
            var response = await openAiTranslateService.TranslateToEnglishAsync(text);
            ViewBag.text = text;
            ViewBag.response = response;
            return View();
        }
    }
}
