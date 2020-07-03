using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Contract;

namespace TweeterBook.Controllers
{
    public class TestController:Controller
    {
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(new { name = "kalai" });
        }
    }
}
