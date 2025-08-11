using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace DotnetAI.WebUI.Services.OpenAINewsSummarizeServices
{
    public class NewsSummarizeService(HttpClient client): INewsSummarizeService
    {

        public async Task<List<string>> FetchLatestNewsAsync()
        {
            string rssFeedUrl = "https://www.sozcu.com.tr/rss/tum-haberler.xml";

            string rssContent =  await client.GetStringAsync(rssFeedUrl);

            var doc = XDocument.Parse(rssContent);

            var items = doc.Descendants("item").Take(10);

            List<string> articles = items.Select(item =>
            {
                string title = item.Element("title")?.Value ?? string.Empty;
                string description = item.Element("description")?.Value ?? string.Empty;
                return $"{ title} \n { description }";
            }).ToList();

            return articles;
        }


        public async Task<List<string>> SummarizeLatestNewsAsync()
        {
            var articles = await FetchLatestNewsAsync();

            List<string> summarizedArticles = new List<string>();


            foreach (var article in articles)
            {
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                {
                    new {role="system",content="You are an expert news summarizer."},
                    new {role="user",content= $"Bu haberi 3 cümlede özetle: {article}"},

                },
                    max_tokens = 500,
                };


                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("chat/completions", content);

                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ChatResponseDto>(responseContent);
                    var summarizedText = result.Choices[0].Message.Content;
                    summarizedArticles.Add(summarizedText);
                }
            }





            return summarizedArticles;


        }




    

    }
}
