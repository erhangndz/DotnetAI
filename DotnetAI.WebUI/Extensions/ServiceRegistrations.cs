using DotnetAI.WebUI.Options;
using DotnetAI.WebUI.Services.GoogleCloudVisionServices;
using DotnetAI.WebUI.Services.TesseractOcrServices;

namespace DotnetAI.WebUI.Extensions
{
    public static class ServiceRegistrations
    {

        public static IServiceCollection AddServicesExt(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<OpenAIOptions>(configuration.GetSection(nameof(OpenAIOptions)));
            services.Configure<WhisperAudioOptions>(configuration.GetSection(nameof(WhisperAudioOptions)));
            services.Configure<DallEOptions>(configuration.GetSection(nameof(DallEOptions)));
            services.Configure<GoogleCloudVisionOptions>(configuration.GetSection(nameof(GoogleCloudVisionOptions)));

            services.AddScoped<IOcrService, OcrService>();
            services.AddScoped<IGoogleCloudVisionService, GoogleCloudVisionService>();

            return services;
        }

    }
}
