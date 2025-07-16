using DotnetAI.WebUI.DTOs.WhisperAudioDtos;
using DotnetAI.WebUI.Services.WhisperAudioServices;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class AudioToTextController(IWhisperAudioService audioService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile audioFile)
        {
            var response = await audioService.ConvertAudioToTextAsync(audioFile);
            return View(response);
        }
    }
}
