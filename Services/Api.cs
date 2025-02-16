using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DashboardTienda.Models;
using System.Text.Json;
using DotNetEnv;
using System.IO;

namespace DashboardTienda.Services
{
    public class Api
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public Api()
        {
            _httpClient = new HttpClient();
            string path = Path.Combine(Directory.GetCurrentDirectory(), ".env");
            Env.Load(path);
            _apiUrl = Env.GetString("API_URL");
        }

        public async Task<string?> LoginAsync(UserLogin user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiUrl}/login", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent);
                return errorResponse?.message ?? null;
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
