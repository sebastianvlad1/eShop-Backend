using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace eShop_backend.Models{
    public class Cart{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string ownerId { get; set; }

        public List<CartItem> cartItems{ get; set; }
    }
}