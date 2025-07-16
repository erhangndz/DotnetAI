using System.Text;
using System.Text.Json;
using DotnetAI.WebUI.DTOs.DallEDTos;

namespace DotnetAI.WebUI.Services.DallEServices
{
    public class DallEService(HttpClient client): IDallEService
    {
        public async Task<DallEImageResponseDto> GenerateImageAsync(string prompt)
        {
            var requestBody = new
            {
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("images/generations", content);

           
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DallEImageResponseDto>(responseString);
            }

            return null;
        }
    }
}
