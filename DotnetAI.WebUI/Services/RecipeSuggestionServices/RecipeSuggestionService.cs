using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using Newtonsoft.Json;
using System.Text;

namespace DotnetAI.WebUI.Services.RecipeSuggestionServices
{
    public class RecipeSuggestionService(HttpClient client): IRecipeSuggestionService
    {

        public async Task<string> SuggestRecipeWithAIAsync(string ingredients)
        {
            var requestBody = new
            {
                model = "gpt-4-turbo",
                messages = new[]
                {
                    new{role="system",content="You are an expert about giving recipes with the given ingredients"},
                    new{role="user",content=$"Senden bir yemek veya tatlı tarifi vermeni istiyorum içeriğinde sadece şu malzemeler yer almalı: {ingredients}"}
                },
                max_tokens = 1000
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody),Encoding.UTF8,"application/json");

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
