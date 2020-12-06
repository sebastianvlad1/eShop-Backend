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
                productDescription = "Descriere laptop",
                productName = "ASUS ROG 90X",
                pret = 5500,
                category = "Laptop",
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
    }
}
