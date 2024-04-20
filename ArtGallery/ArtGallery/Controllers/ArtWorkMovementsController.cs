using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Entities;

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtWorkMovementsController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;

        public ArtWorkMovementsController(ArtGalleryApiContext context)
        {
            _context = context;
        }

        // GET: api/ArtWorkMovements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtWorkMovement>>> GetArtWorkMovement()
        {
            return await _context.ArtWorkMovement.ToListAsync();
        }

        // GET: api/ArtWorkMovements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtWorkMovement>> GetArtWorkMovement(int id)
        {
            var artWorkMovement = await _context.ArtWorkMovement.FindAsync(id);

            if (artWorkMovement == null)
            {
                return NotFound();
            }

            return artWorkMovement;
        }

        // PUT: api/ArtWorkMovements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtWorkMovement(int id, ArtWorkMovement artWorkMovement)
        {
            if (id != artWorkMovement.Id)
            {
                return BadRequest();
            }

            _context.Entry(artWorkMovement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtWorkMovementExists(id))
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

        // POST: api/ArtWorkMovements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtWorkMovement>> PostArtWorkMovement(ArtWorkMovement artWorkMovement)
        {
            _context.ArtWorkMovement.Add(artWorkMovement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtWorkMovement", new { id = artWorkMovement.Id }, artWorkMovement);
        }

        // DELETE: api/ArtWorkMovements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtWorkMovement(int id)
        {
            var artWorkMovement = await _context.ArtWorkMovement.FindAsync(id);
            if (artWorkMovement == null)
            {
                return NotFound();
            }

            _context.ArtWorkMovement.Remove(artWorkMovement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtWorkMovementExists(int id)
        {
            return _context.ArtWorkMovement.Any(e => e.Id == id);
        }
    }
}
