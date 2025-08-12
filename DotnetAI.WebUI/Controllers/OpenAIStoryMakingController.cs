using DotnetAI.WebUI.DTOs.StoryMakingDtos;
using DotnetAI.WebUI.Services.StoryMakingServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAIStoryMakingController(IStoryMakingService storyMakingService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateStoryDto storyDto)
        {
            var response = await storyMakingService.MakeStoryWitAIAsync(storyDto);
            ViewBag.response= response;
            return View();
        }
    }
}
