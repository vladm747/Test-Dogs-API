using System.Threading.Tasks;
using BLL.DTO;
using BLL.Services.DI.Abstract;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;

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
        public IActionResult GetDogs([FromQuery] string? attribute = null, [FromQuery] string? order = null, [FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {
            var dogs = pageNumber == null && pageSize == null ? _service.GetAllDogs(attribute ?? "name", order) 
                : _service.GetAllDogs(attribute ?? "name", order, pageNumber!.Value, pageSize!.Value);
            return Ok(dogs);
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
