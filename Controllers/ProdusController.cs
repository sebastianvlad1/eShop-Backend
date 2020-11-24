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
                productDescription = "descriere",
                productName = "TV1",
                pret = 479
            };
            return _productsService.addProduct(product);
        }
        [HttpGet("getCart")]
        public Cart getCart([FromQuery] string userId){
            return _productsService.getCart(userId);
        }
    }
}
