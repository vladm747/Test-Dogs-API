using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Services.DI.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Infrastructure.DI.Abstract;
using DAL.Models;
using DogsAPI.Filters;

namespace DogsAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogService _service;

        public DogsController(IDogService service)
        {
            _service = service;
        }

        [HttpGet("dogs")]
        public IActionResult GetDogs()
        {
            return Ok(_service.GetAllDogs());
        }

        [HttpGet("dog/{name}")]
        public async Task<IActionResult> GetDogByName(string name)
        {

            return Ok(await _service.GetDogByNameAsync(name));
        }
        
        [HttpPut("dog/{name}")]
        public async Task<IActionResult> Dog(string name, [FromBody] DogUpdateDTO dog)
        {
            await _service.UpdateDogAsync(name, dog);
            return Ok();
        }
        
        [HttpPost("dog")]
        public async Task<ActionResult<Dog>> AddDog(DogDTO dog)
        {
            await _service.AddDogAsync(dog);
            return CreatedAtAction(nameof(GetDogByName), new { name = dog.Name }, dog);
        }
        
        [HttpDelete("dog/{name}")]
        public async Task<IActionResult> DeleteDog(string name)
        {
            return await _service.DeleteDogAsync(name) ? Ok() : NotFound();
        }
    }
}
