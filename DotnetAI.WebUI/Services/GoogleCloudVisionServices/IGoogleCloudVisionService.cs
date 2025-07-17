namespace DotnetAI.WebUI.Services.GoogleCloudVisionServices
{
    public interface IGoogleCloudVisionService
    {

        Task<string> ReadDataFromImageAsync(IFormFile imageFile);
    }
}
