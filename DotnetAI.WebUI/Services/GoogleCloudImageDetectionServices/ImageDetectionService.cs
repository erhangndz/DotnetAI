using Newtonsoft.Json;
using System.Text;


namespace DotnetAI.WebUI.Services.GoogleCloudImageDetectionServices
{
    public class ImageDetectionService(HttpClient client) : IImageDetectionService
    {

        public async Task<object> DetectObjectsAsync(IFormFile imageFile)
        {
            byte[] imageBytes;

            // 1. Gelen dosyayı diske yazmak yerine doğrudan bir MemoryStream'e kopyalıyoruz.
            // Bu, dosya kilitleme sorununu önler ve daha verimlidir.
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                // MemoryStream'i bir byte dizisine dönüştürüyoruz.
                imageBytes = memoryStream.ToArray();
            }


            
            var base64Image = Convert.ToBase64String(imageBytes);

            var requestBody = new
            {
                requests = new[]
            {
                    new
                    {
                        image= new{ content= base64Image },
                        features= new[]
                        {
                            new{ type= "LABEL_DETECTION", maxResults = 10}
                        }
                    }
                }
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<dynamic>(responseString);

            if (response.IsSuccessStatusCode)
            {
                return responseContent.responses[0].labelAnnotations;
            }

            return $"Bir Hata Oluştu : {responseString}";









        }

    }
}
