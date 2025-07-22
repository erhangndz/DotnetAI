using DotnetAI.WebUI.DTOs.ChatResponseDtos;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;

namespace DotnetAI.WebUI.Services.PdfAnalyzeServices
{
    public class PdfAnalyzeService(HttpClient client): IPdfAnalyzeService
    {
        public async Task<string> ExtractTextFromPdfAndAnalyzeAsync(IFormFile pdfFile)
        {
            var pdfPath = Path.GetTempFileName();
            await using (var stream = new FileStream(pdfPath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }

            var text = new StringBuilder();

            using var pdf = PdfDocument.Open(pdfPath);

            foreach (var page in pdf.GetPages())
            {
                text.AppendLine(page.Text);
            }

            var pdfContent = text.ToString();
           var result = await AnalyzeWithAI(pdfContent);
            return result;

        }

        private async Task<string> AnalyzeWithAI(string text)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new{role="system", content= "Sen bir yapay zeka asistanısın. Kullanıcının gönderdiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver."},
                    new{role="user",content= $"Analyze and summarize the following text : \n\n {text}"}
                },
                
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json,Encoding.UTF8,"application/json");

            var response = await client.PostAsync("chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ChatResponseDto>();
                return result.Choices[0].Message.Content;
            }

            return $"Bir Hata oluştu : {responseString}";

        }
    }
}
