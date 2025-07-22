namespace DotnetAI.WebUI.Services.PdfAnalyzeServices
{
    public interface IPdfAnalyzeService
    {
        Task<string> ExtractTextFromPdfAndAnalyzeAsync(IFormFile pdfFile);
    }
}
