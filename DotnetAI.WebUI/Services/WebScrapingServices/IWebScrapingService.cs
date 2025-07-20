namespace DotnetAI.WebUI.Services.WebScrapingServices
{
    public interface IWebScrapingService
    {
        Task<string> ScrapeAndAnalyzeWebPageAsync(string url);
    }
}
