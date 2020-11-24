using System.Collections.Generic;
using eShop_backend.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace eShop_backend.Services{

    public class ProductsService{
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Cart> _carts;
        public ProductsService(IDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(settings.ProductsCollectionName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
            _carts = database.GetCollection<Cart>(settings.CartsCollectionName);
        }

        public Product addProduct(Product product){
            _products.InsertOne(product);
            return product;
        }
        public User addUser(User user){
            _users.InsertOne(user);
            Cart cart = new Cart{
                ownerId = user.id,
                cartItems = new List<CartItem>()
            };
            _carts.InsertOne(cart);

            return user;
        }
        public User login(User user){
            return _users.Find(x => x.username.Equals(user.username) && x.password.Equals(user.password)).Limit(1).SingleOrDefault();
        }
        public Cart getCart(string userId){
            return _carts.Find(x => x.ownerId.Equals(userId)).Limit(1).SingleOrDefault();
        }
        
    }
}