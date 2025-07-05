using DotnetAI.WebUI.DTOs.OpenAIChatDtos;
using DotnetAI.WebUI.Options;
using DotnetAI.WebUI.Services.OpenAIChatServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAIChatController(IOpenAIChatService chatService) : Controller
    {
    

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ChatResponseDto dto)
        {
            var response = await chatService.SendPromptAsync(dto);
            return View(response);
        }

    }
}
