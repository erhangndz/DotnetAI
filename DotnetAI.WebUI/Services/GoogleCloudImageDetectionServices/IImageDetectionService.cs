namespace DotnetAI.WebUI.Services.GoogleCloudImageDetectionServices
{
    public interface IImageDetectionService
    {
        Task<string> DetectObjectsAsync(IFormFile imageFile);
    }
}
