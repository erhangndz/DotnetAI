using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DotnetAI.WebUI.Services.OpenAITranslateServices
{
    public class OpenAiTranslateService(HttpClient client): IOpenAiTranslateService
    {

        public async Task<string> TranslateToEnglishAsync(string text)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful translator." },
                    new { role = "user", content = $"Please translate this text to English: {text}" }
                }
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseString);
                string translation = responseObject.choices[0].message.content;
                return translation;
            }
            catch (Exception e)
            {
                return $"Bir Hata oluştu: {e.Message}";
            }


           
        }
    }
}
