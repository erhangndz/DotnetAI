using DotnetAI.WebUI.DTOs.WhisperAudioDtos;

namespace DotnetAI.WebUI.Services.WhisperAudioServices
{
    public interface IWhisperAudioService
    {
        Task<WhisperTextResponseDto> ConvertAudioToTextAsync(IFormFile audioFile);
    }
}
