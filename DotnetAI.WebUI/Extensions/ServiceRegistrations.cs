using DotnetAI.WebUI.Options;

namespace DotnetAI.WebUI.Extensions
{
    public static class ServiceRegistrations
    {

        public static IServiceCollection AddServicesExt(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<OpenAIOptions>(configuration.GetSection(nameof(OpenAIOptions)));
            services.Configure<WhisperAudioOptions>(configuration.GetSection(nameof(WhisperAudioOptions)));

            return services;
        }

    }
}
