using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestGenerator.Models;

namespace TestGenerator.Services
{
    public class OpenAIService : ILLMService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly LLMOptions _options;

        public OpenAIService(LLMOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(_options.ApiKey))
                throw new InvalidOperationException("OpenAI API key is required.");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.openai.com/v1/")
            };
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _options.ApiKey);
        }

        public async Task<string> GenerateTextAsync(string prompt)
        {
            var payload = new
            {
                model = _options.Model,
                temperature = _options.Temperature,
                max_tokens = _options.MaxTokens,
                messages = new[]
                {
                    new { role = "system", content = "You are a senior C# QA engineer specializing in xUnit, FluentAssertions, Moq, and FlaUI." },
                    new { role = "user", content = prompt }
                }
            };

            var request = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            for (var attempt = 1; attempt <= _options.RetryCount; attempt++)
            {
                var response = await _httpClient.PostAsync("chat/completions", request);
                if (response.IsSuccessStatusCode)
                {
                    dynamic json = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    return (string)json.choices[0].message.content;
                }

                if ((int)response.StatusCode == 429 && attempt < _options.RetryCount)
                {
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)));
                    continue;
                }

                var body = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"OpenAI request failed ({response.StatusCode}): {body}");
            }

            throw new InvalidOperationException("OpenAI request exhausted retries.");
        }

        public void Dispose() => _httpClient?.Dispose();
    }
}