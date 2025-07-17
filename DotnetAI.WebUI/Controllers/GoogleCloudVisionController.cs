using DotnetAI.WebUI.Services.GoogleCloudVisionServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers;

public class GoogleCloudVisionController(IGoogleCloudVisionService googleCloudVisionService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile imageFile)
    {
        var response = await googleCloudVisionService.ReadDataFromImageAsync(imageFile);
        ViewBag.response = response;
        return View();
    }
}