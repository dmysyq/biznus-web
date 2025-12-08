using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace biznus_web.Services
{
    /// <summary>
    /// Сервис для вызова Web API из серверного приложения
    /// </summary>
    public class ApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiClientService> _logger;
        private string? _token;

        public ApiClientService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<ApiClientService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;

            var baseUrl = _configuration["Api:BaseUrl"] ?? "https://localhost:5001";
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetToken(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                _logger.LogInformation("API Request: GET {Endpoint}", endpoint);
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Request failed: GET {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<T?> PostAsync<T>(string endpoint, object? data = null)
        {
            try
            {
                _logger.LogInformation("API Request: POST {Endpoint}", endpoint);
                
                var json = data != null ? JsonSerializer.Serialize(data) : null;
                var content = json != null 
                    ? new StringContent(json, Encoding.UTF8, "application/json")
                    : null;

                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Request failed: POST {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task<T?> PutAsync<T>(string endpoint, object? data = null)
        {
            try
            {
                _logger.LogInformation("API Request: PUT {Endpoint}", endpoint);
                
                var json = data != null ? JsonSerializer.Serialize(data) : null;
                var content = json != null 
                    ? new StringContent(json, Encoding.UTF8, "application/json")
                    : null;

                var response = await _httpClient.PutAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Request failed: PUT {Endpoint}", endpoint);
                throw;
            }
        }

        public async Task DeleteAsync(string endpoint)
        {
            try
            {
                _logger.LogInformation("API Request: DELETE {Endpoint}", endpoint);
                var response = await _httpClient.DeleteAsync(endpoint);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Request failed: DELETE {Endpoint}", endpoint);
                throw;
            }
        }
    }
}

