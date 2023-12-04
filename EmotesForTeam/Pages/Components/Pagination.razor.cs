using Microsoft.AspNetCore.Components;

namespace EmotesForTeam.Components
{
    public class PaginationBase : ComponentBase
    {
        [Parameter]
        public int CurrentPage { get; set; }

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public EventCallback<int>
    OnPageChanged { get; set; }

    protected string GetButtonClass(int page) =>
    CurrentPage == page ? "p-2 border border-blue-500 bg-blue-500 text-white rounded" : "p-2 border border-gray-300 rounded";

    protected void SelectPage(int page)
    {
    if (page != CurrentPage)
    {
    CurrentPage = page;
    OnPageChanged.InvokeAsync(page);
    }
    }

    protected bool IsFirstPage() => CurrentPage == 1;

    protected bool IsLastPage() => CurrentPage == TotalPages;

    protected void GoToFirstPage() => SelectPage(1);

    protected void GoToLastPage() => SelectPage(TotalPages);
    }
    }
