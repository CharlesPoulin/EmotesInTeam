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
            return NotFound();
        }
        return Ok(card);
    }
}
