using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using HtmlAgilityPack;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using System.Text;
using System.Text.Json;

namespace DotnetAI.WebUI.Services.WebScrapingServices
{
    public class WebScrapingService(HttpClient client) : IWebScrapingService
    {

        public async Task<string> ScrapeAndAnalyzeWebPageAsync(string url)
        {

            var web = new HtmlWeb();
            var doc = web.Load(url);

            var webContent = doc.DocumentNode.SelectSingleNode("//body")?.InnerText;


            var result = await AnalyzeWithAI(webContent);

            return result;


        }

        private async Task<string> AnalyzeWithAI(string text)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new{role="system",content="Sen bir yapay zeka asistanısın. Kullanıcının gönderdiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver."},
                    new{role="user",content= $"Analyze and summarize the following Web Page Content : \n\n {text}"}
                },
                max_tokens = 220
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ChatResponseDto>(responseString);

                return result.Choices[0].Message.Content;

            }

            return $"Bir hata oluştu: {responseString}";
        }



    }
}
