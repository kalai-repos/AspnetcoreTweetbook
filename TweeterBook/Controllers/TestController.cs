using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Contract;
using TweeterBook.Filter;

namespace TweeterBook.Controllers
{
    [ApiKeyAuth]
    public class TestController:Controller
    {
        [HttpGet("api/GetName")]
        public IActionResult Get()
        {
            return Ok(new { name = "kalai" });
        }
    }
}
