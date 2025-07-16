using DotnetAI.WebUI.DTOs.DallEDTos;

namespace DotnetAI.WebUI.Services.DallEServices
{
    public interface IDallEService
    {

        Task<DallEImageResponseDto> GenerateImageAsync(string prompt);
    }
}
