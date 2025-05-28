using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AntiRizzGPT.Services
{
    public class RizzService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _prompt;

        public RizzService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var promptPath = Path.Combine(AppContext.BaseDirectory, "prompts", "modes.json");

            if (!File.Exists(promptPath))
                throw new FileNotFoundException("Prompt not found: " + promptPath);

            var json = File.ReadAllText(promptPath);
            var promptData = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (promptData == null || !promptData.ContainsKey("default"))
                throw new InvalidOperationException("Default-prompt is missing");

            _prompt = promptData["default"];
        }

        public async Task<string> GenerateRizzAsync(string userMessage)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("API key not configured");
            
            var requestBody = new
            {
                model = "gpt-4o",
                temperature = 1.3,
                messages = new[]
                {
                    new { role = "system", content = _prompt },
                    new { role = "user", content = userMessage }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);
            var message = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return message?.Trim() ?? "Rizz AI wasn't rizzy. Try again";
        }
    }
}
