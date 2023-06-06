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
        public IActionResult GetDogs([FromQuery] string attribute = "name", [FromQuery] string order = "asc", [FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {
            try
            {
                var dogs = pageNumber == null && pageSize == null
                    ? _service.GetAllDogs(attribute, order)
                    : _service.GetAllDogs(attribute, order, pageNumber!.Value, pageSize!.Value);
                return Ok(dogs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dog/{name}")]
        public async Task<IActionResult> GetDogByName(string name)
        {
            return Ok(await _service.GetDogByNameAsync(name));
        }

        [HttpPut("dog/{name}")]
        public async Task<IActionResult> Dog(string name, [FromBody] DogUpdateDTO dog)
        {
            var result = await _service.UpdateDogAsync(name, dog);
            return Ok(result);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> AddDog(DogDTO dog)
        {
            var result = await _service.AddDogAsync(dog);
            return CreatedAtAction(nameof(GetDogByName), new { name = dog.Name }, result);
        }

        [HttpDelete("dog/{name}")]
        public async Task<IActionResult> DeleteDog(string name)
        {
            return await _service.DeleteDogAsync(name) ? NoContent() : NotFound();
        }
    }
}
