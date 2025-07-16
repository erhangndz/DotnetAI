namespace DotnetAI.WebUI.Services.TesseractOcrServices
{
    public interface IOcrService
    {
        public Task<string> ReadDataFromImageAsync(IFormFile imageFile);
    }
}
