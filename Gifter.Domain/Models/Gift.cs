using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Gifter.Domain.Models
{
    public class Gift
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("items")]
        public HashSet<GiftItem> Items { get; set; }

        [BsonElement("multiple")]
        public bool Multiple { get; set; }

        [BsonElement("share_link")]
        public string ShareLink { get; set; }

        [BsonElement("consumed")]
        public int Consumed { get; set; }

        [BsonElement("max_opening")]
        public int MaxOpening { get; set; }

        [BsonElement("user_id")]
        public string UserId {get;set; }
    }
}
