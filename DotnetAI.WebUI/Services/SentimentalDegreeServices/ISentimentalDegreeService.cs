using DotnetAI.WebUI.DTOs.SentimentalDegreeDtos;

namespace DotnetAI.WebUI.Services.SentimentalDegreeServices
{
    public interface ISentimentalDegreeService
    {

        Task<SentimentalDegreeResponseDto> CalculateAdvancedSentimentAsync(string text);
    }
}
