namespace DotnetAI.WebUI.Services.OpenAINewsSummarizeServices
{
    public interface INewsSummarizeService
    {
        Task<List<string>> FetchLatestNewsAsync();
        Task<List<string>> SummarizeLatestNewsAsync();
    }
}
