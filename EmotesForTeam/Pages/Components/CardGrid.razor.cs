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
        public AuthenticationService AuthenticationService { get; set; } // Inject the AuthenticationService
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<Card>? cards;
        public List<CardViewModel> cardViewModels;
        private string currentUserId;

        private const int PageSize = 30;
        public int CurrentPage { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            currentUserId = await LocalStorage.GetItemAsync<string>("userId");
            await LoadCards();
        }

        public async Task LoadCards()
        {
            if (CardService != null)
            {
                cards = await CardService.GetCardsAsync(CurrentPage, PageSize);
                cardViewModels = cards.Select(card => new CardViewModel { Card = card }).ToList();
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

            var response = await AuthenticationService.AddCardToUser(currentUserId, cardViewModel.Card.Id);
            if (response.IsSuccessStatusCode)
            {
                cardViewModel.isAdded = true; 
            }
            else
            {
                Console.WriteLine("Failed to add card: " + response.StatusCode);
            }
            
        }
    }
}
