using DotnetAI.WebUI.Services.TextToSpeechServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class TextToSpeechController(ITextToSpeechService textToSpeechService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string text)
        {
            await textToSpeechService.ConvertTextToSpeechAsync(text);
            return View();
        }
    }
}
