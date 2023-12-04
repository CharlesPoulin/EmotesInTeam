namespace ApiCardEmotes
{
    public class Card
    {
        public required int Id { get; set; }
        public required string ImageUrl { get; set; }
        public required string Title { get; set; }
        public  required string Owner { get; set; }
        // Add other properties as necessary
    }

}
