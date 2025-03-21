﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DashboardTienda.Models;
using System.Text.Json;
using DotNetEnv;
using System.IO;
using System.Windows;

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
            try
            {
                var userJson = JsonSerializer.Serialize(user);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync($"{_apiUrl}/login", content);

                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)httpResponse.StatusCode;
                    return apiResponse;
                }
                return null;
            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }

        public async Task<ApiResponse?> GetProducts()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/products");

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;


            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }

        }

        public async Task<ApiResponse?> GetCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/categories");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                    if (apiResponse != null)
                    {
                        apiResponse.status = (int)response.StatusCode;
                    }
                    return apiResponse;
                }
                return null;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }

        public async Task<ApiResponse?> GetComments()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/comments");

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;


            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }

        }

        public async Task<ApiResponse?> GetProductsByCategory(int category_id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/categories/{category_id}/products");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                    if (apiResponse != null)
                    {
                        apiResponse.status = (int)response.StatusCode;
                    }
                    return apiResponse;
                }
                return null;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }


        //private
        private string GetToken()
        {
            return TokenService.Instance.Token;
        }
      
        public async Task<ApiResponse?> GetUserById(string id)
        {

            var httpResponse = await _httpClient.GetAsync($"{_apiUrl}/users/{id}");
            return null;
        }
       
        public async Task<ApiResponse?> GetOrders()
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiUrl}/orders");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                    if (apiResponse != null)
                    {
                        apiResponse.status = (int)response.StatusCode;
                    }
                    return apiResponse;
                }
                return null;
            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }

        }
       
        public async Task<ApiResponse?> GetUsers()
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiUrl}/users");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                    if (apiResponse != null)
                    {
                        apiResponse.status = (int)response.StatusCode;
                    }
                    return apiResponse;
                }
                return null;
            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }
      
        public async Task<ApiResponse?> UpdateUser(string originalMail, User updatedUser)
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                var userJson = JsonSerializer.Serialize(updatedUser);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Put, $"{_apiUrl}/users/by-email/{originalMail}")
                {
                    Content = content
                };


                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }

        public async Task<ApiResponse?> UpdateProduct(Product updatedProduct)
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                var userJson = JsonSerializer.Serialize(updatedProduct);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Put, $"{_apiUrl}/products/{updatedProduct._id}")
                {
                    Content = content
                };


                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }

        public async Task<ApiResponse?> UpdateOrder(Order updatedOrder)
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                var userJson = JsonSerializer.Serialize(updatedOrder);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Put, $"{_apiUrl}/orders/{updatedOrder._id}")
                {
                    Content = content
                };


                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }

        public async Task<ApiResponse?> UpdateUserStatus(string user_id, bool block)
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                var statusObject = new
                {
                    blocked = block,
                    block_date = block ? "permanently" : "none"
                };

                var statusJson = JsonSerializer.Serialize(statusObject);
                var content = new StringContent(statusJson, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Patch, $"{_apiUrl}/users/{user_id}/status")
                {
                    Content = content
                };

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }

        public async Task<ApiResponse?> UpdateUserRole(string user_id, string selectedRole)
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                var roleObject = new { role = selectedRole };
                var roleJson = JsonSerializer.Serialize(roleObject);
                var content = new StringContent(roleJson, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Patch, $"{_apiUrl}/users/{user_id}/role")
                {
                    Content = content
                };



                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }

        public async Task<ApiResponse?> DeleteComment(string comment_id)
        {
            try
            {
                var token = GetToken() ?? string.Empty;
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiUrl}/comments/{comment_id}");
                


                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonString, options);
                if (apiResponse != null)
                {
                    apiResponse.status = (int)response.StatusCode;
                }
                return apiResponse;

            }
            catch
            {
                MessageBox.Show("Error de conexión");
                return null;
            }
        }
    }
}
