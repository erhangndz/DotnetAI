﻿using System.Net.Http.Headers;
using DotnetAI.WebUI.Options;
using DotnetAI.WebUI.Services.DallEServices;
using DotnetAI.WebUI.Services.OpenAIChatServices;
using DotnetAI.WebUI.Services.OpenAITextToSpeechServices;
using DotnetAI.WebUI.Services.OpenAITranslateServices;
using DotnetAI.WebUI.Services.SentimentAIServices;
using DotnetAI.WebUI.Services.SentimentalDegreeServices;
using DotnetAI.WebUI.Services.SummarizeArticleServices;
using DotnetAI.WebUI.Services.WhisperAudioServices;

namespace DotnetAI.WebUI.Extensions
{
    public static class HttpClientServices
    {

        public static IServiceCollection AddHttpClientServicesExt(this IServiceCollection services, IConfiguration configuration)
        {

            var openAiOptions = configuration.GetSection(nameof(OpenAIOptions)).Get<OpenAIOptions>();
            var whisperAudioOptions = configuration.GetSection(nameof(WhisperAudioOptions)).Get<WhisperAudioOptions>();
            var dallEOptions = configuration.GetSection(nameof(DallEOptions)).Get<DallEOptions>();
            var openAiTranslateOptions = configuration.GetSection(nameof(OpenAITranslateOptions)).Get<OpenAITranslateOptions>();
            var openAiTextToSpeechOptions = configuration.GetSection(nameof(OpenAITextToSpeechOptions)).Get<OpenAITextToSpeechOptions>();
            var sentimentAiOptions = configuration.GetSection(nameof(SentimentAiOptions)).Get<SentimentAiOptions>();
            var sentimentalDegreeOptions = configuration.GetSection(nameof(SentimentalDegreeOptions)).Get<SentimentalDegreeOptions>();
            var summarizeArticleOptions = configuration.GetSection(nameof(SummarizeArticleOptions)).Get<SummarizeArticleOptions>();

            services.AddHttpClient<IOpenAIChatService, OpenAIChatService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiOptions.OpenRouterKey}");
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

            services.AddHttpClient<IOpenAiTranslateService, OpenAiTranslateService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiTranslateOptions.ApiKey}");
                opt.BaseAddress = new Uri("https://api.openai.com/v1/");
            });

            services.AddHttpClient<IOpenAiTextToSpeechService, OpenAiTextToSpeechService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiTextToSpeechOptions.ApiKey}");
                opt.BaseAddress = new Uri("https://api.openai.com/v1/");
            });

            services.AddHttpClient<ISentimentAiService, SentimentAiService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization", $"Bearer {sentimentAiOptions.ApiKey}");
                opt.BaseAddress = new Uri("https://api.openai.com/v1/");
            });

            services.AddHttpClient<ISentimentalDegreeService, SentimentalDegreeService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization", $"Bearer {sentimentalDegreeOptions.ApiKey}");
                opt.BaseAddress = new Uri("https://api.openai.com/v1/");
            });

            services.AddHttpClient<ISummarizeArticleService, SummarizeArticleService>(opt =>
            {
                opt.DefaultRequestHeaders.Add("Authorization",$"Bearer {summarizeArticleOptions.ApiKey}");
                opt.BaseAddress = new Uri("https://api.openai.com/v1/");
            });


            return services;
        }
    }
}
