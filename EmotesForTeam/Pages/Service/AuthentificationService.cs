using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace EmotesForTeam.Pages.Service
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Users/login", loginRequest);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Login failed with status code: {response.StatusCode} {response.ReasonPhrase}");
                    return false;
                }

                var token = await response.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("authToken", token);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return false;
            }
        }



        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }

        public async Task<bool> IsUserLoggedIn()
        {
            return await _localStorage.GetItemAsync<string>("authToken") != null;
        }
    }
}