using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop_backend.Models;
using eShop_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace eShop_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProdusController : ControllerBase
    {
        private readonly ProductsService _productsService;
        public ProdusController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("addproduct")]
        public Product addProduct()
        {
            Product product = new Product(){
                productDescription = "Lenovo Legion y520 laptop de gaming i5-7800HQ, GTX 1060 4gb, 16gb ram",
                productName = "Lenovo Legionr",
                pret = 3999,
                category = "Laptopuri",
                onsale = false
            };
            return _productsService.addProduct(product);
        }
        [HttpGet("getall")]
        public List<Product> getAll(){
            return _productsService.getAll();
        }
        public class AddObj{
            public string productId { get; set; }
            public string userId { get; set; }
        }
        [HttpPost("addtocart")]
        public ActionResult addToCart([FromBody] AddObj obj){
            Console.WriteLine("Am primit productid: " + obj.productId + " pt user: " + obj.userId);
            _productsService.addToCart(obj.productId, obj.userId);
            return Ok();
        }
        [HttpPost("getcart")]
        public ActionResult getCart([FromBody] string userId){
            Console.WriteLine("Caut cart pt user: " + userId);
            return Ok(_productsService.getCart(userId));
        }
        [HttpPost("detelecartitem")]
        public IActionResult deteleCartItem([FromBody] AddObj obj){
            Console.WriteLine("Sterge produsul " + obj.productId + " de la userul " + obj.userId);
            _productsService.deleteCartItem(obj.userId, obj.productId);
            return Ok();
        }
        [HttpGet("getproducts")]
        public IActionResult getProducts([FromQuery] string search, string category)
        {
            Console.WriteLine("CONTROLLER. search: " + search + ", category: " + category);
            return Ok(_productsService.getProductsByCategory(search, category));
        }
        [HttpPost("cleanCart")]
        public IActionResult cleanCart([FromBody] string userId){
            Cart cart = _productsService.getSimpleCart(userId);
            cart.cartItems = new List<CartItem>();
            _productsService.editCart(cart);
            return Ok();
        }
        [HttpPost("creeazaComanda")]
        public IActionResult creeazaComanda([FromBody] Comanda comanda){
            Console.WriteLine("CREEZ COMANDA");
            comanda.data = DateTime.Now;
            return Ok(_productsService.creeazaComanda(comanda));
        }
        [HttpGet("getUserOrders")]
        public IActionResult getUserOrders([FromQuery] string userId){
            return Ok(_productsService.getUserOrders(userId));
        }
        [HttpGet("getOrderDetails")]
        public IActionResult getOrderDetails([FromQuery] string orderId){
            return Ok(_productsService.getOrderDetails(orderId));
        }
    }
}
