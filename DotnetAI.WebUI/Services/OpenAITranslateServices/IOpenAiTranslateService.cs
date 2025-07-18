namespace DotnetAI.WebUI.Services.OpenAITranslateServices
{
    public interface IOpenAiTranslateService
    {

        Task<string> TranslateToEnglishAsync(string text);
    }
}
