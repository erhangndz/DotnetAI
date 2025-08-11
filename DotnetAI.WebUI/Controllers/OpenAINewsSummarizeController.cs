using DotnetAI.WebUI.Services.OpenAINewsSummarizeServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAINewsSummarizeController(INewsSummarizeService newsSummarizeService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await newsSummarizeService.SummarizeLatestNewsAsync();
            ViewBag.response = response;
            return View();
        }

        
    }
}
