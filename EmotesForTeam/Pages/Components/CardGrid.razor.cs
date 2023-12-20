// Update these using statements to match the actual namespaces of Card and CardService
using EmotesForTeam.Model; // Adjusted namespace for the Card model
using EmotesForTeam.Services;    // Assuming CardService is in this namespace
using Microsoft.AspNetCore.Components;

namespace EmotesForTeam.Components
{
    public class CardGridBase : ComponentBase
    {
        [Inject]
        protected CardService? CardService { get; set; }

        protected List<Card>? cards;
        protected const int PageSize = 30; // 6x5 grid

        protected int CurrentPage { get; set; } = 1;

        // Declare the OnPageChanged event callback
        [Parameter]
        public EventCallback<int> OnPageChanged { get; set; }


        protected async Task NextPage()
        {
            CurrentPage++;
            await LoadCards();
            _ = OnPageChanged.InvokeAsync(CurrentPage); // Notify the parent component (if needed)
        }

        protected async Task PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadCards();
                _ = OnPageChanged.InvokeAsync(CurrentPage); // Notify the parent component (if needed)
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadCards();
        }

        private async Task LoadCards()
        {
            cards = await CardService.GetCardsAsync(CurrentPage, PageSize);
        }
    }
}