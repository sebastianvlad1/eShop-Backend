using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace eShop_backend.Models{

    public class CartItem{

        [BsonRepresentation(BsonType.ObjectId)]
        public string productId { get; set; }
        
        public int number { get; set; }
    }
}