
using System.Text;
using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using DotnetAI.WebUI.DTOs.OpenAIChatDtos;
using DotnetAI.WebUI.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DotnetAI.WebUI.Services.OpenAIChatServices
{
    public class OpenAIChatService(HttpClient client) : IOpenAIChatService
    {
        
        public async Task<OpenAIChatRequestDto> SendPromptAsync(OpenAIChatRequestDto dto)
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

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync("chat/completions",content);
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ChatResponseDto>(responseString);

                    var chatResponse1 = new OpenAIChatRequestDto
                    {
                        Response = result.Choices[0].Message.Content,
                        Prompt = dto.Prompt
                    };
                  
                    return chatResponse1;
                }
                var message = await response.Content.ReadAsStringAsync();
                if (message.Contains("insufficient_quota"))
                {
                    // Kullanıcıya göster
                    var chatResponseInsufficient = new OpenAIChatRequestDto
                    {
                        Response = "OpenAI API kullanım kotan dolmuş.Lütfen planını kontrol et.",
                        Prompt = dto.Prompt
                    };
                    return chatResponseInsufficient;
                }
                var chatResponse2 = new OpenAIChatRequestDto
                {
                    
                    Response =
                        $"Bir Hata Oluştu! Hata Kodu: {response.StatusCode} - {message}",
                    Prompt = dto.Prompt
                };
                return chatResponse2;
            }
            catch (Exception e)
            {

                var chatResponse3 = new OpenAIChatRequestDto
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
