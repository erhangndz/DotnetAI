using DotnetAI.WebUI.Services.SentimentalDegreeServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class SentimentalDegreeController(ISentimentalDegreeService sentimentalDegreeService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string text)
        {
            var response = await sentimentalDegreeService.CalculateAdvancedSentimentAsync(text);
            if (response is null)
            {
                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");
                return View(text);
            }


            ViewBag.text = text;
   
            return View(response);
        }
    }
}
