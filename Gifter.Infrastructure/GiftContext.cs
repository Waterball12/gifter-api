using Gifter.Domain.Models;
using Gifter.Domain.Options;
using MongoDB.Driver;

namespace Gifter.Infrastructure
{
    public class GiftContext
    {
        public IMongoCollection<Gift> Gift { get; }

        public GiftContext(GiftContextOptions options)
        {
            
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                new MongoUrl(options.DatabaseConnection));

            settings.AllowInsecureTls = true;

            var mongo = new MongoClient(settings);

            //var mongo = new MongoClient(options.DatabaseConnection);

            var db = mongo.GetDatabase("gifter");

            Gift = db.GetCollection<Gift>("gift");
        }
    }
}
