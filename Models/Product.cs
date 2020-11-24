using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop_backend.Models{
    public class Product{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string productName { get; set; }
        public string productDescription {get; set;}
        public float pret {get; set;}
    
    }
}