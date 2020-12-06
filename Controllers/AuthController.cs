using eShop_backend.Models;
using eShop_backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace eShop_backend.Controllers{

    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase{

        private readonly ProductsService _productsService;

        public AuthController(ProductsService productsService){
            _productsService = productsService;
        }
        
       [HttpPost("register")]
       public User register([FromForm] User registeredUser){
           User user = new User(){
               username = registeredUser.username,
               password = registeredUser.password,
               email = registeredUser.email,
               role = "user"
           };
           return _productsService.addUser(user);
       }
       [HttpPost("login")]
        public ActionResult Login([FromForm] User loginUser){
           var res =  _productsService.login(loginUser);
           if(res == null){
               return BadRequest("Login failed");
           }else{
                return Ok(res);
           }
        }
        [HttpPost("getuser")]
        public IActionResult getUser([FromBody] string userId){
            Console.WriteLine("Caut user cu id: " + userId);
            return Ok(_productsService.getUser(userId));
        }
    }
}