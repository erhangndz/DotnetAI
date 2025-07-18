namespace DotnetAI.WebUI.Services.SentimentAIServices
{
    public interface ISentimentAiService
    {
        Task<string> AnalyzeSentimentAsync(string text);
    }
}
