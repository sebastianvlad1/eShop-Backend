using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop_backend.Models{
    public class User{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string username { get; set; }
        public string password {get; set;}
        public string email { get; set;}
        public string role {get; set;}
        public string adresa { get; set;}
        public string codPostal { get; set;}
        public string tara { get; set; }
        public string judet  { get; set; }
        public string oras { get; set; }
    }
}