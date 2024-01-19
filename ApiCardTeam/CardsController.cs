using ApiCardEmotes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly CardService _cardService;

    public CardsController(CardService cardService)
    {
        _cardService = cardService;
    }

    
    // public async Task<HttpResponseMessage> RemoveCardFromUser(string userId, string cardId)
    // {
    //     // Construct the URL with the userId in the path and cardId as a query parameter
    //     var url = $"api/Users/{userId}/removecard?cardId={cardId}";
    //
    //     // Send a POST request to the constructed URL
    //     var response = await _httpClient.PostAsync(url, null); // null indicates no content in the body of the request
    //     return response;
    // }

    // Update this method to include pagination
    [HttpGet]
    public async Task<ActionResult<List<Card>>> GetAllCards([FromQuery] int page = 1, [FromQuery] int pageSize = 30)
    {
        var cards = await _cardService.GetPaginatedCardsAsync(page, pageSize);
        return Ok(cards);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Card>> GetCardById(string id)
    {
        var card = await _cardService.GetCardById(id);
        if (card == null)
        {
            return NotFound();  // Return 404 if the card doesn't exist
        }
        return Ok(card);  // Return 200 with the card data
    }
}
