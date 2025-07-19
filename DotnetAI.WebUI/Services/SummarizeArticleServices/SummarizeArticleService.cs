using System.Text;
using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DotnetAI.WebUI.Services.SummarizeArticleServices
{
    public class SummarizeArticleService(HttpClient client) : ISummarizeArticleService
    {

        public async Task<string> SummarizeTextAsync(string text,string level)
        {
       
            string instruction = level switch
            {
                "short" => "Summarize this text in 1 - 2 sentences : ",
                "medium" => "Summarize this text in 3-4 sentences : ",
                "detailed" => "Give a detailed summary of this text : ",
                _ => "Summarize this text."
            };


            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content =
                            "You are an AI that summarize texts info different levels: short, medium and detailed. "
                    },
                    new { role = "user", content = $"{instruction}   {text}" }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("chat/completions",content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChatResponseDto>(responseString);
                return result.Choices[0].Message.Content;
            }

            return $"Bir Hata Oluştu : {responseString}";
        }

    }
}
