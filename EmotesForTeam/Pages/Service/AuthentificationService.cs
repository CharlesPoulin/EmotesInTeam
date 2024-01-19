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
        public event Action OnAuthenticationStateChanged;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        
        public class LoginResponse
        {
            public string Token { get; set; }
            public string UserId { get; set; }
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

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse != null)
                {
                    await _localStorage.SetItemAsync("authToken", loginResponse.Token);
                    await _localStorage.SetItemAsync("userId", loginResponse.UserId);
                    return true;
                }

                return false;
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
            OnAuthenticationStateChanged?.Invoke();
        }

        public async Task<bool> IsLoggedIn()
        {
            return await _localStorage.GetItemAsync<string>("authToken") != null;
        }

        public async Task<bool> Register(string username, string email, string password)
        {
            var registerRequest = new { Username = username, Email = email, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Users/register", registerRequest);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Register failed with status code: {response.StatusCode} {response.ReasonPhrase}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register error: {ex.Message}");
                return false;
            }
        }
        
        public async Task<HttpResponseMessage> AddCardToUser(string userId, string cardId)
        {
            var url = $"/Users/addcard";

            var payload = new { cardId = cardId, userId = userId };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, payload);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error adding card: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddCardToUser: {ex.Message}");
                return null;
            }
        }


    }
}