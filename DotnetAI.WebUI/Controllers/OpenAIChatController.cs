using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using DotnetAI.WebUI.DTOs.OpenAIChatDtos;
using DotnetAI.WebUI.Options;
using DotnetAI.WebUI.Services.OpenAIChatServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using static Google.Rpc.Context.AttributeContext.Types;

namespace DotnetAI.WebUI.Controllers
{
    public class OpenAIChatController(IOpenAIChatService chatService) : Controller
    {
        private const string ChatHistoryKey = "ChatHistory";

        [HttpGet]
        public IActionResult Index()
        {
            var chatHistory = HttpContext.Session.GetString(ChatHistoryKey);
            var messages = string.IsNullOrEmpty(chatHistory)
            ? new List<ChatMessagesDto>()
            : JsonSerializer.Deserialize<List<ChatMessagesDto>>(chatHistory);

            return View(messages);
            
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SendMessage([FromBody] OpenAIChatRequestDto request)
        {
            // Session'dan mevcut sohbet geçmişini al
            var chatHistoryJson = HttpContext.Session.GetString(ChatHistoryKey);
            var messages = string.IsNullOrEmpty(chatHistoryJson)
                ? new List<ChatMessagesDto>()
                : JsonSerializer.Deserialize<List<ChatMessagesDto>>(chatHistoryJson);

            // Kullanıcının mesajını geçmişe ekle
            messages.Add(new ChatMessagesDto { Author = "User", Content = request.Prompt, Timestamp = DateTime.Now });

            // OpenAI servisine isteği gönder (mevcut chatService'inizi kullanarak)
           
            var responseDto = await chatService.SendPromptAsync(new OpenAIChatRequestDto { Prompt=request.Prompt});


            // Bot'un cevabını oluştur
            var botMessage = new ChatMessagesDto
            {
                Author = "Bot",
                Content = responseDto.Response, 
                Timestamp = DateTime.Now
            };

            // Bot'un cevabını da geçmişe ekle
            messages.Add(botMessage);

            // Güncellenmiş sohbet geçmişini tekrar Session'a kaydet
            HttpContext.Session.SetString(ChatHistoryKey, JsonSerializer.Serialize(messages));

            // Sadece bot'un yeni cevabını JSON olarak client'a geri dön
            return Json(botMessage);
        }

    }
}
