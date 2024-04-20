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
    public class ArtWorksController : ControllerBase
    {
        private readonly ArtGalleryApiContext _context;

        public ArtWorksController(ArtGalleryApiContext context)
        {
            _context = context;
        }

        // GET: api/ArtWorks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtWork>>> GetArtWork()
        {
            return await _context.ArtWork.ToListAsync();
        }

        // GET: api/ArtWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtWork>> GetArtWork(int id)
        {
            var artWork = await _context.ArtWork.FindAsync(id);

            if (artWork == null)
            {
                return NotFound();
            }

            return artWork;
        }

        // PUT: api/ArtWorks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtWork(int id, ArtWork artWork)
        {
            if (id != artWork.Id)
            {
                return BadRequest();
            }

            _context.Entry(artWork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtWorkExists(id))
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

        // POST: api/ArtWorks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtWork>> PostArtWork(ArtWork artWork)
        {
            _context.ArtWork.Add(artWork);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtWork", new { id = artWork.Id }, artWork);
        }

        // DELETE: api/ArtWorks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtWork(int id)
        {
            var artWork = await _context.ArtWork.FindAsync(id);
            if (artWork == null)
            {
                return NotFound();
            }

            _context.ArtWork.Remove(artWork);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtWorkExists(int id)
        {
            return _context.ArtWork.Any(e => e.Id == id);
        }
    }
}
