using DotnetAI.WebUI.Services.GoogleCloudImageDetectionServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotnetAI.WebUI.Controllers
{
    public class GoogleCloudImageDetectionController(IImageDetectionService imageDetectionService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Index(IFormFile imageFile)
        {
            var response = await imageDetectionService.DetectObjectsAsync(imageFile);
            ViewBag.response = response;
            return View();

        }
    }
}
