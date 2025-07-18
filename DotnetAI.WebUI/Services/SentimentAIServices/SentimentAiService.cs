using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DotnetAI.WebUI.Services.SentimentAIServices
{
    public class SentimentAiService(HttpClient client): ISentimentAiService
    {
        public async Task<string> AnalyzeSentimentAsync(string text)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content =
                            "You are an AI that analyzes sentiment. You categorize a text as Positive, Negative or Neutral"
                    },
                    new
                    {
                        role = "user",
                        content =
                            $"Analyze the sentiment of this text: \"{text}\" and return only Positive, Negative or Neutral "
                    }
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
               
                var result = JsonConvert.DeserializeObject<dynamic>(responseString);
                return result.choices[0].message.content.ToString();
            }

            return $"Bir Hata Oluştu: {responseString}";

        } 

    }
}
