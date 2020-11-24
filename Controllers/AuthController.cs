using eShop_backend.Models;
using eShop_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShop_backend.Controllers{

    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase{

        private readonly ProductsService _productsService;

        public AuthController(ProductsService productsService){
            _productsService = productsService;
        }
        
       [HttpPost("register")]
       public User register([FromForm]User user){
           return _productsService.addUser(user);
       }
       [HttpPost("login")]
        public ActionResult Login([FromForm] User user){
           var res =  _productsService.login(user);
           if(res == null){
               return BadRequest("Login failed");
           }else{
                return Ok();
           }
        }
    }
}