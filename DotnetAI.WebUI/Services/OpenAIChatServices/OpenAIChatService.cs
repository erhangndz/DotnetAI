
using System.Text;
using System.Text.Json;
using DotnetAI.WebUI.DTOs.OpenAIChatDtos;
using DotnetAI.WebUI.Options;
using Microsoft.Extensions.Options;

namespace DotnetAI.WebUI.Services.OpenAIChatServices
{
    public class OpenAIChatService(HttpClient client) : IOpenAIChatService
    {
        
        public async Task<ChatResponseDto> SendPromptAsync(ChatResponseDto dto)
        {
            var requestBody = new
            {
                model = "gpt-4.1",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = dto.Prompt },

                },
                max_tokens = 1024
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("",content);
                if (response.IsSuccessStatusCode)
                {
                    var result =await  response.Content.ReadFromJsonAsync<JsonElement>();

                    var chatResponse1 = new ChatResponseDto
                    {
                        Response =
                            result.GetProperty("choices")[0].GetProperty("message").GetProperty("content")
                                .GetString(),
                        Prompt = dto.Prompt
                    };
                  
                    return chatResponse1;
                }
                var message = await response.Content.ReadAsStringAsync();
                if (message.Contains("insufficient_quota"))
                {
                    // Kullanıcıya göster
                    var chatResponseInsufficient = new ChatResponseDto
                    {
                        Response = "OpenAI API kullanım kotan dolmuş.Lütfen planını kontrol et.",
                        Prompt = dto.Prompt
                    };
                    return chatResponseInsufficient;
                }
                var chatResponse2 = new ChatResponseDto
                {
                    
                    Response =
                        $"Bir Hata Oluştu! Hata Kodu: {response.StatusCode} - {message}",
                    Prompt = dto.Prompt
                };
                return chatResponse2;
            }
            catch (Exception e)
            {

                var chatResponse3 = new ChatResponseDto
                {
                    Response =
                        $"Bir Hata Oluştu! {e}",
                    Prompt = dto.Prompt
                };
                return chatResponse3;

            }
        }
    }
}
