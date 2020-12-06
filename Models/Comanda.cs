using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace eShop_backend.Models{
    public class DetaliiProdus{
        public string denumire { get; set; }
        public float pret { get; set; }
        public int numar { get; set; }
    }
    public class Comanda{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ownerId { get; set; }
        public string nume { get; set; }
        public string adresa { get; set; }
        public string plata { get; set; }
        public string status { get; set; }
        public DateTime data { get; set;}
        public float suma { get; set; }
        public List<DetaliiProdus> produse{ get; set;}
    }
}