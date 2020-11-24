using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShop_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProdusController : ControllerBase
    {

        public ProdusController()
        {
        }

        [HttpGet("test")]
        public String Get()
        {
            return "Merge";
        }
    }
}
