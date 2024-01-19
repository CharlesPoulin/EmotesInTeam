using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver; 

public class UserRepository
{
    private readonly IMongoCollection<ApplicationUser> _usersCollection;

    public UserRepository(IMongoClient client)
    {
        var database = client.GetDatabase("UserInformation");
        _usersCollection = database.GetCollection<ApplicationUser>("Users");
    }

    public void AddUser(ApplicationUser user)
    {
        _usersCollection.InsertOne(user);
    }
    public bool UserExists(string username, string email)
    {
        var usernameFilter = Builders<ApplicationUser>.Filter.Eq(u => u.UserName, username);
        var emailFilter = Builders<ApplicationUser>.Filter.Eq(u => u.Email, email);
        var combinedFilter = Builders<ApplicationUser>.Filter.Or(usernameFilter, emailFilter);

        return _usersCollection.Find(combinedFilter).Any();
    }
    public ApplicationUser GetUserByUsername(string username)
    {
        return _usersCollection.Find(u => u.UserName == username).FirstOrDefault();
    }
    public void AddCardToUser(string userId, string cardId)
    {
        var filter = Builders<ApplicationUser>.Filter.Eq(u => u.Id, userId);

        var user = _usersCollection.Find(filter).FirstOrDefault();
        if (user != null && user.CardInventoryIds == null)
        {
            // If the array is null, initialize it with the cardId
            var initArrayUpdate = Builders<ApplicationUser>.Update.Set(u => u.CardInventoryIds, new List<string> { cardId });
            _usersCollection.UpdateOne(filter, initArrayUpdate);
        }
        else
        {
            // If the array exists, use AddToSet to add the cardId
            var addToSetUpdate = Builders<ApplicationUser>.Update.AddToSet(u => u.CardInventoryIds, cardId);
            _usersCollection.UpdateOne(filter, addToSetUpdate);
        }
    }





}