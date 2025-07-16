using DotnetAI.WebUI.Services.DallEServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class DallEController(IDallEService dallEService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string prompt)
        {
            var response = await dallEService.GenerateImageAsync(prompt);
            if (response != null)
            {
                return View(response);
            }

            return View();



        }
    }
}
