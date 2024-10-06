using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LunaSenatiConnection.Models;
namespace LunaSenatiConnection.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.ApiBaseUrl) // Usa la URL base dinámica
            };
        }

        // Método de login
        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            var loginData = new { email = email, password = password };
            var jsonContent = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                Console.WriteLine("Iniciando solicitud de login...");
                Console.WriteLine($"URL de la solicitud: {_httpClient.BaseAddress}auth/login");
                Console.WriteLine($"Contenido del request: {jsonContent}");

                var response = await _httpClient.PostAsync("api/auth/login", content);

                Console.WriteLine($"Respuesta HTTP: {response.StatusCode}");

                // Leer la respuesta como string
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenido de la respuesta: {responseString}");

                if (response.IsSuccessStatusCode)
                {
                    // Deserializar el contenido JSON a un objeto AuthResponse
                    var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseString);
                    return authResponse;
                }
                else
                {
                    Console.WriteLine($"Error en la solicitud: {response.StatusCode} - {responseString}");
                    return null;
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Error en la conexión HTTP: {httpEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Otro error: {ex.Message}");
                return null;
            }
        }

    }
}