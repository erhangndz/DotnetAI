using DotnetAI.WebUI.Options;
using Google.Cloud.Vision.V1;
using Microsoft.Extensions.Options;

namespace DotnetAI.WebUI.Services.GoogleCloudVisionServices;

public class GoogleCloudVisionService(IOptions<GoogleCloudVisionOptions> googleCloudVisionOptions): IGoogleCloudVisionService
{
    private readonly GoogleCloudVisionOptions _googleCloudVisionOptions = googleCloudVisionOptions.Value;
    public async Task<string> ReadDataFromImageAsync(IFormFile imageFile)
    {
        // Save the uploaded file to a temporary location
        var imagePath = Path.GetTempFileName();
        await using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        string credentialPath = _googleCloudVisionOptions.CredentialPath;

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        try
        {
            var client = await ImageAnnotatorClient.CreateAsync();
            var image = Image.FromFile(imagePath);
            var response = await client.DetectTextAsync(image);
            string responseText = "";
            foreach (var annotation in response)
            {
                if (!string.IsNullOrEmpty(annotation.Description))
                {
                    responseText = annotation.Description;
                    return responseText;
                }


            }

            return responseText;
        }
        catch (Exception e)
        {
            return $"Bir hata oluştu : {e.Message}";
        }
    }
}