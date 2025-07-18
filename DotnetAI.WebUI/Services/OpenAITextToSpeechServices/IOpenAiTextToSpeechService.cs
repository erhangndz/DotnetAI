namespace DotnetAI.WebUI.Services.OpenAITextToSpeechServices
{
    public interface IOpenAiTextToSpeechService
    {
        Task<bool> GenerateSpeechAsync(string text, string voiceType);
    }
}
