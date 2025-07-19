namespace DotnetAI.WebUI.Services.SummarizeArticleServices
{
    public interface ISummarizeArticleService
    {
        Task<string> SummarizeTextAsync(string text, string level);
    }
}
