using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiCardEmotes
{
    public class Card
    {
        // Use ObjectId for the Id property and mark it as the BSON identifier
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonRequired]
        public string ImageUrl { get; set; }

        [BsonRequired]
        public string Title { get; set; }

        [BsonRequired]
        public string Owner { get; set; }

        // Add other properties as necessary
    }
}