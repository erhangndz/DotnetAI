using DotnetAI.WebUI.Services.OpenAITextToSpeechServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAITextToSpeechController(IOpenAiTextToSpeechService openAiTextToSpeechService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(string text, string voiceType)
        {
            var result = await openAiTextToSpeechService.GenerateSpeechAsync(text,voiceType);
            if (result is false)
            {
                ModelState.AddModelError("","Bir Hata Oluştu");
                return View(text);
            }

            ViewBag.text = text;
            return View();
        }

    }
}
