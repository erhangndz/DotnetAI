using System.Text;
using DotnetAI.WebUI.DTOs.SentimentalDegreeDtos;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DotnetAI.WebUI.Services.SentimentalDegreeServices
{
    public class SentimentalDegreeService(HttpClient client) : ISentimentalDegreeService 
    {

        public async Task<SentimentalDegreeResponseDto> CalculateAdvancedSentimentAsync(string text)
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
                            "You are an advanced AI that analyzes emotions from text. Your response must be in Json Format. Identify the sentiment scores(0-100%) for the following emotions: Joy, Sadness, Anger, Fear, Surprised and Neutral. and also state if it involves hate speech as Yes or No. This Hate Speech property name must be like:  hasHateSpeech: Yes/No. Json Format must be as following example : { \"joy\":10, \"sadness\":10, \"anger\":10, \"fear\": 10, \"surprised\":10,\"neutral\": 10, \"hasHateSpeech\": \"Yes\"}"
                    },
                    new
                    {
                        role = "user",
                        content =
                            $"Analyze this text: \"{text}\" and return a JSON object with percentages for each emotions and also state if this text involves a hate speech or not only by Yes or No value in Json object"
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<dynamic>(responseString);
                var jsonResult = result.choices[0].message.content.ToString();
                return JsonConvert.DeserializeObject<SentimentalDegreeResponseDto>(jsonResult);
            }

            return null;
        }
    }
}
