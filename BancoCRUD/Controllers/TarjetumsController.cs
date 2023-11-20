using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BancoCRUD.Models;

namespace BancoCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetumsController : ControllerBase
    {
        private readonly BancobdContext _context;

        public TarjetumsController(BancobdContext context)
        {
            _context = context;
        }

        // GET: api/Tarjetums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarjetum>>> GetTarjeta()
        {
          if (_context.Tarjeta == null)
          {
              return NotFound();
          }
            return await _context.Tarjeta.ToListAsync();
        }

        // GET: api/Tarjetums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarjetum>> GetTarjetum(int id)
        {
          if (_context.Tarjeta == null)
          {
              return NotFound();
          }
            var tarjetum = await _context.Tarjeta.FindAsync(id);

            if (tarjetum == null)
            {
                return NotFound();
            }

            return tarjetum;
        }

        // PUT: api/Tarjetums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarjetum(int id, Tarjetum tarjetum)
        {
            if (id != tarjetum.IdTarjeta)
            {
                return BadRequest();
            }

            _context.Entry(tarjetum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetumExists(id))
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

        // POST: api/Tarjetums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tarjetum>> PostTarjetum(Tarjetum tarjetum)
        {
          if (_context.Tarjeta == null)
          {
              return Problem("Entity set 'BancobdContext.Tarjeta'  is null.");
          }
            _context.Tarjeta.Add(tarjetum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarjetum", new { id = tarjetum.IdTarjeta }, tarjetum);
        }

        // DELETE: api/Tarjetums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjetum(int id)
        {
            if (_context.Tarjeta == null)
            {
                return NotFound();
            }
            var tarjetum = await _context.Tarjeta.FindAsync(id);
            if (tarjetum == null)
            {
                return NotFound();
            }

            _context.Tarjeta.Remove(tarjetum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarjetumExists(int id)
        {
            return (_context.Tarjeta?.Any(e => e.IdTarjeta == id)).GetValueOrDefault();
        }
    }
}
