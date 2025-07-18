using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace DotnetAI.WebUI.Services.OpenAITextToSpeechServices
{
    public class OpenAiTextToSpeechService(HttpClient client): IOpenAiTextToSpeechService
    {

        public async Task<bool> GenerateSpeechAsync(string text,string voiceType)
        {

            //alloy => kadın  onyx => erkek
            var fixedText = ReverseTextForRTL(text);

            var requestBody = new
            {
                model = "tts-1",
                input = fixedText,
                voice = voiceType
            };

            string json = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("audio/speech", content);
            if (response.IsSuccessStatusCode)
            {
                var audioBytes = await response.Content.ReadAsByteArrayAsync();
                var currentDirectory = Directory.GetCurrentDirectory();
                var audioFileName = Guid.NewGuid() + ".mp3";
                var saveLocation = Path.Combine(currentDirectory,"wwwroot" ,"audio-output", audioFileName);
                await File.WriteAllBytesAsync(saveLocation, audioBytes);
                Process.Start("explorer.exe", saveLocation);
                return true;
            }

            Debug.WriteLine("Bir Hata Oluştu");
            return false;





        }


        static bool ContainsRTLCharacters(string text)
        {
            foreach (char c in text)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category == UnicodeCategory.OtherLetter) // RTL diller genellikle "OtherLetter" kategorisine girer
                {
                    return true;
                }
            }
            return false;
        }

        static string ReverseTextForRTL(string text)
        {
            // Arapça veya İbranice veya farklı bir RTL dili içerip içermediğini kontrol et
            if (ContainsRTLCharacters(text))
            {
                char[] array = text.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }
            return text; // Eğer RTL değilse olduğu gibi bırak
        }
    }
}
