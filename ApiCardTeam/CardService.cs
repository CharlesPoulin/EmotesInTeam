using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiCardEmotes
{
    public class CardService
    {
        private readonly MongoDbContext _dbContext;

        public CardService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Card>> GetAllCardsAsync()
        {
            return await _dbContext.Cards.Find(_ => true).ToListAsync();
        }

        public async Task<List<Card>> GetPaginatedCardsAsync(int page, int pageSize)
        {
            return await _dbContext.Cards.Find(card => true)
                                     .Skip((page - 1) * pageSize)
                                     .Limit(pageSize)
                                     .ToListAsync();
        }
        
        public async Task<Card> GetCardById(string id)
        {
            // Fetch the card from the database using a string ID
            return await _dbContext.Cards.Find(card => card.Id == id).FirstOrDefaultAsync();
        }


    }

}
