using Microsoft.AspNetCore.Mvc;
using ViccAdatbazis.Data;

namespace ViccAdatbazis.Controllers
{
    public class JokesController : ControllerBase
    {
        private readonly ViccDbContext _context;

        public JokesController(ViccDbContext context)
        {
            _context = context;
        }

        // Add like to a joke
        [HttpPut("{id}/like")]
        public async Task<IActionResult> AddLike(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }

            if (joke.Aktiv)
            {
                return BadRequest("Cannot like/dislike an archived joke.");
            }

            joke.Tetszik += 1;
            await _context.SaveChangesAsync();

            return Ok(joke);
        }

        // Add dislike to a joke
        [HttpPut("{id}/dislike")]
        public async Task<IActionResult> AddDislike(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }

            joke.NemTetszik += 1;
            await _context.SaveChangesAsync();

            return Ok(joke);
        }

    }
}
