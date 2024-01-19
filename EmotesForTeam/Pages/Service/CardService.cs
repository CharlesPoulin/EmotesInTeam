using System.Net.Http.Json;
using EmotesForTeam.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;

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
            try
            {
                // Replace with your actual API endpoint
                var response = await _httpClient.GetAsync($"api/Cards?page={page}&size={size}");

                if (response.IsSuccessStatusCode)
                {
                    var cards = await response.Content.ReadFromJsonAsync<List<Card>>();
                    return cards ?? new List<Card>();
                }
                else
                {
                    // Handle non-success status code. Log it, return an empty list, or throw an exception as needed.
                    Console.WriteLine($"API call failed: {response.StatusCode}");
                    return new List<Card>();
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HttpRequestException. Log it, return an empty list, or throw an exception as needed.
                Console.WriteLine($"Exception during API call: {ex.Message}");
                return new List<Card>();
            }
        }
        
        public async Task<Card> GetCardById(string cardId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Cards/{cardId}");

                if (response.IsSuccessStatusCode)
                {
                    var card = await response.Content.ReadFromJsonAsync<Card>();
                    return card;
                }
                else
                {
                    Console.WriteLine($"API call failed: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Exception during API call: {ex.Message}");
                return null;
            }

        }
  

    }
}
