using System.Net.Http.Json;
using EmotesForTeam.Model;
using EmotesForTeam.Services;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using EmotesForTeam.Pages.Service;
namespace EmotesForTeam.Pages.Components
{
    public partial class CardGridBase : ComponentBase
    {
        [Inject]
        public CardService? CardService { get; set; }
        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }
        [Inject]
        public AuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpClient HttpClient { get; set; }

        public List<Card>? cards;
        public List<CardViewModel> cardViewModels;
        private string currentUserId;
        private List<string> userCardInventory;


        private const int PageSize = 30;
        public int CurrentPage { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            currentUserId = await LocalStorage.GetItemAsync<string>("userId");
            userCardInventory = await GetUserCardInventory(); // Fetch the user's card inventory
            await LoadCards();
        }

        public async Task LoadCards()
        {
            if (CardService != null)
            {
                cards = await CardService.GetCardsAsync(CurrentPage, PageSize);
                cardViewModels = cards.Select(card => 
                    new CardViewModel 
                    { 
                        Card = card,
                        isAdded = userCardInventory.Contains(card.Id) // Set based on the user's inventory
                    }).ToList();
            }
        }
        
        public void NavigateToCardDetail(string cardId)
        {
            NavigationManager.NavigateTo($"/cardDetail/{cardId}");
        }

        public async Task NextPage()
        {
            CurrentPage++;
            await LoadCards();
        }

        public async Task PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadCards();
            }
        }

        public bool IsFirstPage => CurrentPage <= 1;
        public bool IsLastPage => cards != null && cards.Count < PageSize;


        public async Task QuickAddCard(CardViewModel cardViewModel)
        {
            if (AuthenticationService == null)
            {
                Console.WriteLine("AuthenticationService is not available.");
                return;
            }

            if (!cardViewModel.isAdded)
            {
                var response = await AuthenticationService.AddCardToUser(currentUserId, cardViewModel.Card.Id);
                if (response.IsSuccessStatusCode)
                {
                    cardViewModel.isAdded = true;
                    // Optionally, add the cardId to userCardInventory list
                    userCardInventory.Add(cardViewModel.Card.Id);
                }
                else
                {
                    Console.WriteLine("Failed to add card: " + response.StatusCode);
                }
            }
        }
        
        private async Task<List<string>> GetUserCardInventory()
        {
            try
            {
                // Correctly use the injected httpClient here
                var response = await HttpClient.GetAsync($"/api/Users/{currentUserId}/cards");
        
                if (response.IsSuccessStatusCode)
                {
                    var userCards = await response.Content.ReadFromJsonAsync<List<string>>();
                    return userCards ?? new List<string>();
                }
                else
                {
                    Console.WriteLine($"Failed to fetch card inventory: {response.StatusCode}");
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in fetching card inventory: {ex.Message}");
                return new List<string>();
            }
        }

    }
}
