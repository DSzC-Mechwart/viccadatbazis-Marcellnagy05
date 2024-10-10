using Microsoft.AspNetCore.Mvc;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;
using Microsoft.EntityFrameworkCore;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        //Database connection
        private readonly ViccDbContext _context;

        public JokesController(ViccDbContext context)
        {
            _context = context;
        }

        //Getting all jokes
        [HttpGet]
        public async Task<ActionResult<List<Vicc>>> GetJokes()
        {
            return await _context.Viccek.Where(x => x.Aktiv == true).ToListAsync();
        }

        //Getting one joke
        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GetJoke(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }
            return joke;
        }

        //Adding new joke
        [HttpPost]
        public async Task<ActionResult<Vicc>> PostJoke(Vicc joke)
        {
            _context.Viccek.Add(joke);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJoke", new { id = joke.Id }, joke);
        }

        //Edit Joke
        [HttpPut("{id}")]
        public async Task<ActionResult<Vicc>> PutJoke(int id, Vicc joke)
        {
            if (id != joke.Id)
            {
                return BadRequest();
            }
            _context.Entry(joke).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete Joke

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vicc>> DeleteJoke(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }
            if (joke.Aktiv == true) 
            { 
                joke.Aktiv = false;
            }
            else
            {
                _context.Viccek.Remove(joke);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
