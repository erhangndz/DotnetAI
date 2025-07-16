using System.Text.Json;
using DotnetAI.WebUI.DTOs.WhisperAudioDtos;

namespace DotnetAI.WebUI.Services.WhisperAudioServices
{
    public class WhisperAudioService(HttpClient client) : IWhisperAudioService
    {
       
        public async Task<WhisperResponseDto> ConvertAudioToTextAsync(IFormFile audioFile)
        {

            if (audioFile == null || audioFile.Length == 0)
            {
                throw new ArgumentException("Ses dosyası boş veya mevcut değil.", nameof(audioFile));
            }

            // Ses dosyasını MemoryStream'e kopyala
            using var memoryStream = new MemoryStream();
            await audioFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0; // Ensure the stream is at the beginning
            var audioContent = new ByteArrayContent(memoryStream.ToArray());

            var form = new MultipartFormDataContent();
            form.Add(audioContent, "file", audioFile.FileName);
            form.Add(new StringContent("whisper-1"),"model");
            var response = await client.PostAsync("audio/transcriptions", form);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API isteği başarısız oldu: {response.StatusCode} - {errorContent}");
            }

            var responseString = await response.Content.ReadAsStringAsync();

           
            try
            {

               
                var whisperResponse = JsonSerializer.Deserialize<WhisperResponseDto>(responseString);
                return whisperResponse;

            }
            catch (Exception ex)
            {
                throw new Exception($"Whisper yanıtını işlerken hata oluştu: {ex.Message} - Yanıt: {responseString}", ex);
            }
        }
    }
}
