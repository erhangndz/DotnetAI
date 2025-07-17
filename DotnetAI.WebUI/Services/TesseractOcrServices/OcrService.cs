using Tesseract;

namespace DotnetAI.WebUI.Services.TesseractOcrServices
{
    public class OcrService: IOcrService
    {


        public async Task<string> ReadDataFromImageAsync(IFormFile imageFile)
        {
            // Save the uploaded file to a temporary location
            var imagePath = Path.GetTempFileName();
            await using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var tessDataPath = @"C:\tessdata";

            try
            {
                using var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default);
                using var img = Pix.LoadFromFile(imagePath);
                using var page = engine.Process(img);
                string text = page.GetText(); 
                return text;
            }
            catch (Exception e)
            {
                return $"Bir hata oluştu : {e.Message}";
            }


        }

    }
}
