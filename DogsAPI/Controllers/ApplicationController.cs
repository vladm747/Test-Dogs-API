using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogsAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Index()
        {
            return Ok("Dogs house service. Version 1.0.1");
        } 
    }
}
