using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Models;

namespace DogsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogContext _context;

        public DogsController(DogContext context)
        {
            _context = context;
        }

        // GET: api/Dogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> Dogs()
        {
            i
        }

        // GET: api/Dogs/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Dog>> Dog(string name)
        {
            if (_context.Dogs == null)
            {
                return NotFound();
            }
            var dog = await _context.Dogs.FindAsync(name);

            if (dog == null)
            {
                return NotFound();
            }

            return dog;
        }

        // PUT: api/Dogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> Dog(string name, Dog dog)
        {
            if (name != dog.Name)
            {
                return BadRequest();
            }

            _context.Entry(dog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dog>> Dog(Dog dog)
        {
            if (_context.Dogs == null)
            {
                return Problem("Entity set 'DogContext.Dogs'  is null.");
            }
            _context.Dogs.Add(dog);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DogExists(dog.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Dog", new { name = dog.Name }, dog);
        }

        // DELETE: api/Dogs/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteDog(string name)
        {
            if (_context.Dogs == null)
            {
                return NotFound();
            }
            var dog = await _context.Dogs.FindAsync(name);
            if (dog == null)
            {
                return NotFound();
            }

            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DogExists(string name)
        {
            return (_context.Dogs?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}
