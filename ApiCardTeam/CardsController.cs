using ApiCardEmotes;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly List<Card> _cards; // This should ideally come from a database

    public CardsController()
    {
        // Initialize with some sample data
        _cards =
        [
            new() { Id = 1, Title = "Card 1", ImageUrl = "https://cdn.7tv.app/emote/638767f24cc489ef45239272/4x.webp", Owner = "Admin"},
            new() { Id = 2, Title = "Card 2", ImageUrl = "https://cdn.7tv.app/emote/6042089e77137b000de9e669/4x.webp" ,Owner = "Admin"},
            new() { Id = 3, Title = "Card 3", ImageUrl = "https://cdn.7tv.app/emote/60be7f41ae67b5b98b425a70/4x.webp", Owner = "Admin"},
            new() { Id = 4, Title = "Card 4", ImageUrl = "https://cdn.7tv.app/emote/639ae69c6364fad576b0ea0d/4x.webp", Owner = "Admin"},
                   ];
    }

    [HttpGet]
    public ActionResult<List<Card>> Get(int page, int size)
    {
        var pagedCards = _cards.Skip((page - 1) * size).Take(size).ToList();
        return Ok(pagedCards);
    }
}
