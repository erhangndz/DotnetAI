using DotnetAI.WebUI.Services.TesseractOcrServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class TesseractOcrController(IOcrService ocrService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile imageFile)
        {
            var response = await ocrService.ReadDataFromImageAsync(imageFile);
            ViewBag.response = response;
            return View();
        }
    }
}
