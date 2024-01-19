using EmotesForTeam.Model;
using EmotesForTeam.Services;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;

namespace EmotesForTeam.Pages.Components
{
    public partial class CardGridBase : ComponentBase
    {
        [Inject]
        public CardService? CardService { get; set; }
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }
        
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
            await LoadFavorites();
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

        public async Task ToggleFavorite(CardViewModel cardViewModel)
        {
            cardViewModel.IsFavorite = !cardViewModel.IsFavorite;
            await LocalStorage.SetItemAsync("favorites", cardViewModels);
        }

        public async Task LoadFavorites()
        {
            var storedFavorites = await LocalStorage.GetItemAsync<List<CardViewModel>>("favorites");
            if (storedFavorites != null)
            {
                foreach (var cardViewModel in cardViewModels)
                {
                    var storedFavorite = storedFavorites.FirstOrDefault(c => c.Card.Id == cardViewModel.Card.Id);
                    if (storedFavorite != null)
                    {
                        cardViewModel.IsFavorite = storedFavorite.IsFavorite;
                    }
                }
            }
        }

        public string GetHeartImageUrl(bool isFavorite)
        {
            return isFavorite ? "/Images/svg/heart-full.svg" : "/Images/svg/heart-empty.svg";
        }
        
        public async Task QuickAddCard(string cardId)
        {
            if (CardService == null)
            {
                Console.WriteLine("CardService is not available.");
                return;
            }

            var response = await CardService.AddCardToUser(currentUserId, cardId);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Card added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add card.");
            }
        }
    }
}
