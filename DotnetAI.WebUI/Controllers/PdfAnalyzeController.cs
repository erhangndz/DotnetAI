using DotnetAI.WebUI.Services.PdfAnalyzeServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class PdfAnalyzeController(IPdfAnalyzeService pdfAnalyzeService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile pdfFile)
        {
            var response = await pdfAnalyzeService.ExtractTextFromPdfAndAnalyzeAsync(pdfFile);
            ViewBag.response= response;
            return View();
        }
    }
}
