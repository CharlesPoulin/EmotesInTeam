using ApiCardEmotes;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Buffers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly MongoDbContext _context;

    public CardsController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Card>>> Get(int page, int size)
    {
        var pagedCards = await _context.Cards.Find(_ => true).Skip((page - 1) * size).Limit(size).ToListAsync();
        return Ok(pagedCards);
    }
}

