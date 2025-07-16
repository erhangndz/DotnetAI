using System.Net.Http.Headers;
using DotnetAI.WebUI.Options;
using DotnetAI.WebUI.Services.DallEServices;
using DotnetAI.WebUI.Services.OpenAIChatServices;
using DotnetAI.WebUI.Services.WhisperAudioServices;

namespace DotnetAI.WebUI.Extensions
{
    public static class HttpClientServices
    {
        
        public static IServiceCollection AddHttpClientServicesExt(this IServiceCollection services,IConfiguration configuration)
        {
           
            var openAiOptions = configuration.GetSection(nameof(OpenAIOptions)).Get<OpenAIOptions>();
            var whisperAudioOptions = configuration.GetSection(nameof(WhisperAudioOptions)).Get<WhisperAudioOptions>();
            var dallEOptions = configuration.GetSection(nameof(DallEOptions)).Get<DallEOptions>();

            services.AddHttpClient<IOpenAIChatService, OpenAIChatService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization",$"Bearer {openAiOptions.OpenRouterKey}");
                //opt.BaseAddress = new Uri("https://api.openai.com/v1/chat/completions");
                opt.BaseAddress = new Uri("https://openrouter.ai/api/v1/chat/completions");
            });

            services.AddHttpClient<IWhisperAudioService, WhisperAudioService>(opt =>
            {
                opt.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", whisperAudioOptions.ApiKey);
                opt.BaseAddress = new Uri("https://api.openai.com/v1/");
            });

            services.AddHttpClient<IDallEService, DallEService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization", $"Bearer {dallEOptions.ApiKey}");
                opt.BaseAddress = new Uri("https://api.openai.com/v1/");
            });


            return services;
        }
    }
}
