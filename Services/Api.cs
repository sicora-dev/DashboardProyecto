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

        //public

        public async Task<ApiResponse?> Login(UserLogin user)
        {
            var userJson = JsonSerializer.Serialize(user);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");
            var httpResponse = await _httpClient.PostAsync($"{_apiUrl}/login", content);

            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
            if (apiResponse != null)
            {
                apiResponse.status = (int)httpResponse.StatusCode;
            }

            return apiResponse;

        }
        

        //private

        public async Task<ApiResponse?> GetUserById(string id)
        {
            
            var httpResponse = await _httpClient.GetAsync($"{_apiUrl}/user/{id}");
            return null;
        }
    }
}
