namespace DotnetAI.WebUI.Services.GoogleCloudImageDetectionServices
{
    public interface IImageDetectionService
    {
        Task<object> DetectObjectsAsync(IFormFile imageFile);
    }
}
