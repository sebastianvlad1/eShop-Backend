using System.Collections.Generic;
using eShop_backend.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace eShop_backend.Services{

    public class ProductsService{
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Cart> _carts;
        private readonly IMongoCollection<Comanda> _comenzi;
        public ProductsService(IDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(settings.ProductsCollectionName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
            _carts = database.GetCollection<Cart>(settings.CartsCollectionName);
            _comenzi = database.GetCollection<Comanda>(settings.ComenziCollectionName);
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
        public class CartItemsShow{
            public Product product { get; set;}
            public int number { get; set;}
        }
        public Cart getSimpleCart(string userId){
            return _carts.Find(x => x.ownerId.Equals(userId)).Limit(1).SingleOrDefault();
        }
        public List<Comanda> getUserOrders(string userId){
            return _comenzi.Find(c => c.ownerId.Equals(userId)).SortByDescending(c => c.id).Limit(50).ToList();
        }
        public List<CartItemsShow> getCart(string userId){
            Cart cart =  _carts.Find(x => x.ownerId.Equals(userId)).Limit(1).SingleOrDefault();
            List<CartItemsShow> cartItemsShows = new List<CartItemsShow>();
            foreach(CartItem item in cart.cartItems){
                Product product = _products.Find(p => p.id == item.productId).Limit(1).FirstOrDefault();
                CartItemsShow cartItemShow = new CartItemsShow(){
                    product = product,
                    number = item.number
                };
                cartItemsShows.Add(cartItemShow);
            }
            return cartItemsShows;
        }
        public User getUser(string id){
            return _users.Find(x => x.id == id).Limit(1).SingleOrDefault();
        }
        public List<Product> getAll(){
            return _products.Find(_ => true).ToList();
        }
        public List<Product> getProductsByCategory(string search, string category){
            if(category == null && search == null){
                return _products.Find(p => p.productName.ToLower().Contains("")).Limit(15).ToList(); 
            }else if(category == null){
                return _products.Find(p => p.productName.ToLower().Contains(search)).Limit(15).ToList();
            }else if(search == null){
                return _products.Find(p => p.productName.ToLower().Contains("") && p.category.ToLower() == category.ToLower()).Limit(15).ToList();
            }else{
                 return _products.Find(p => p.productName.ToLower().Contains(search) && p.category.ToLower() == category.ToLower()).Limit(15).ToList();
            }
            
        }
        public void addToCart(string productId, string userId){
            Cart cart = _carts.Find(c => c.ownerId == userId).Limit(1).FirstOrDefault();
            CartItem cartItem = cart.cartItems.Find(i => i.productId == productId);
            if(cartItem != null){
                int index = cart.cartItems.IndexOf(cartItem);
                cart.cartItems[index].number = cart.cartItems[index].number + 1;

            }else{
                CartItem item = new CartItem(){
                    productId = productId,
                    number = 1
                };
                cart.cartItems.Add(item);
            }
                var filter = Builders<Cart>.Filter.Eq("_id", new ObjectId(cart.id));
                var update = Builders<Cart>.Update.Set("cartItems", cart.cartItems);
                _carts.UpdateOne(filter,update);
        }
        public void editCart(Cart cart){
            var filter = Builders<Cart>.Filter.Eq("_id", new ObjectId(cart.id));
            var update = Builders<Cart>.Update.Set("cartItems", cart.cartItems);
            _carts.UpdateOne(filter,update);
        }
        public Comanda creeazaComanda(Comanda comanda){
            _comenzi.InsertOne(comanda);
            return comanda;
        }
        public void deleteCartItem(string userId, string productId){
            Cart cart = _carts.Find(c => c.ownerId == userId).Limit(1).FirstOrDefault();
            CartItem cartItem = cart.cartItems.Find(i => i.productId == productId);
            cart.cartItems.Remove(cartItem);
            var filter = Builders<Cart>.Filter.Eq("_id", new ObjectId(cart.id));
            var update = Builders<Cart>.Update.Set("cartItems", cart.cartItems);
            _carts.UpdateOne(filter,update);
        }
        public Comanda getOrderDetails(string orderId){
            return _comenzi.Find(c => c.id.Equals(orderId)).SingleOrDefault();
        }
        public Product getProductById(string productId){
            return _products.Find(c => c.id.Equals(productId)).SingleOrDefault();
        }
    }
}