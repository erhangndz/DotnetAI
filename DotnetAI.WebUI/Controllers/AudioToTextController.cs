using DotnetAI.WebUI.DTOs.WhisperAudioDtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class AudioToTextController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AudioConvertRequestDto requestDto)
        {
            return View();
        }
    }
}
