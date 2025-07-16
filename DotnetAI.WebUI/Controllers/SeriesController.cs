using System.Text.Json;
using DotnetAI.WebUI.DTOs.ImdbDtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAI.WebUI.Controllers
{
    public class SeriesController(HttpClient client) : Controller
    {


        public async Task<IActionResult> Index()
        {
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb236.p.rapidapi.com/api/imdb/most-popular-tv"),
                Headers =
                {
                    { "x-rapidapi-key", "e782ab3024msh2e78af442950fd0p115136jsnc3dd0b8c5f54" },
                    { "x-rapidapi-host", "imdb236.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonSerializer.Deserialize<List<ApiSeriesDto>>(body);
                return View(values);
            }
        
        }
    }
}
