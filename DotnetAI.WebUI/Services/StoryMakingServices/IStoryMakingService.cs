using DotnetAI.WebUI.DTOs.StoryMakingDtos;

namespace DotnetAI.WebUI.Services.StoryMakingServices
{
    public interface IStoryMakingService
    {
        Task<string> MakeStoryWitAIAsync(CreateStoryDto storyDto);
    }
}
