using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using DotnetAI.WebUI.DTOs.StoryMakingDtos;
using Newtonsoft.Json;
using System.Text;

namespace DotnetAI.WebUI.Services.StoryMakingServices
{
    public class StoryMakingService(HttpClient client): IStoryMakingService
    {

        public async Task<string> MakeStoryWitAIAsync(CreateStoryDto storyDto)
        {
            string prompt = $"{storyDto.Genre} türünde bir hikaye yaz. Ana karakterin adı: {storyDto.MainCharacter}. Hikaye {storyDto.Setting} bölgesinde geçiyor. {storyDto.Length} bir hikaye olsun. Giriş, gelişme ve sonuç içermeli. ";

            var requestBody = new
            {
                model="gpt-4-turbo",
                messages = new[]
                {
                    new{role="system",content="You are a creative story writer."},
                    new{role="user",content=prompt}
                },
                max_tokens= 1000
            };


            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("chat/completions", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChatResponseDto>(responseContent);

                return result.Choices[0].Message.Content;
            }

            return $"Bir Hata Oluştu: {responseContent}";
       
        }

    }
}
