namespace DotnetAI.WebUI.Services.TextToSpeechServices
{
    public interface ITextToSpeechService
    {

        Task ConvertTextToSpeechAsync(string text);
    }
}
