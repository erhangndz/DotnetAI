using DotnetAI.WebUI.DTOs.OpenAIChatDtos;

namespace DotnetAI.WebUI.Services.OpenAIChatServices
{
    public interface IOpenAIChatService
    {
        Task<OpenAIChatRequestDto> SendPromptAsync(OpenAIChatRequestDto dto);
    }
}
