using System.Speech.Synthesis;

namespace DotnetAI.WebUI.Services.TextToSpeechServices
{
    public class TextToSpeechService(SpeechSynthesizer speechSynthesizer): ITextToSpeechService
    {

        public Task ConvertTextToSpeechAsync(string text)
        {
            speechSynthesizer.Volume = 100;
            speechSynthesizer.Rate = -2;

            if (!string.IsNullOrEmpty(text))
            {
               speechSynthesizer.Speak(text);
              
            }

            return Task.CompletedTask;
        }

    }
}
