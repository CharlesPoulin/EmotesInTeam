using System.Net.Http.Json;
using EmotesForTeam.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmotesForTeam.Services
{
    public class CardService
    {
        private readonly HttpClient _httpClient;

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Card>> GetCardsAsync(int page, int size)
        {
            // Replace with your actual API endpoint
            var response = await _httpClient.GetFromJsonAsync<List<Card>>($"api/Cards?page={page}&size={size}");
            return response ?? new List<Card>();
        }
    }
}
