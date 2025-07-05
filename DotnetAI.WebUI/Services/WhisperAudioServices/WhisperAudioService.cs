using System.Net.Http.Headers;
using DotnetAI.WebUI.DTOs.WhisperAudioDtos;

namespace DotnetAI.WebUI.Services.WhisperAudioServices
{
    public class WhisperAudioService(HttpClient client): IWhisperAudioService
    {
        public async Task<WhisperTextResponseDto> ConvertAudioToTextAsync(IFormFile audioFile)
        {

            var audioContent = new ByteArrayContent(File.ReadAllBytes(audioFile.FileName));
            audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse("audio/mpeg");
            var form = new MultipartFormDataContent();

            return new WhisperTextResponseDto();
        }
    }
}
