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
    public class TransaccionsController : ControllerBase
    {
        private readonly BancobdContext _context;

        public TransaccionsController(BancobdContext context)
        {
            _context = context;
        }

        // GET: api/Transaccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccions()
        {
          if (_context.Transaccions == null)
          {
              return NotFound();
          }
            return await _context.Transaccions.ToListAsync();
        }

        // GET: api/Transaccions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaccion>> GetTransaccion(int id)
        {
          if (_context.Transaccions == null)
          {
              return NotFound();
          }
            var transaccion = await _context.Transaccions.FindAsync(id);

            if (transaccion == null)
            {
                return NotFound();
            }

            return transaccion;
        }

        // PUT: api/Transaccions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaccion(int id, Transaccion transaccion)
        {
            if (id != transaccion.IdTransaccion)
            {
                return BadRequest();
            }

            _context.Entry(transaccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionExists(id))
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

        // POST: api/Transaccions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaccion>> PostTransaccion(Transaccion transaccion)
        {
          if (_context.Transaccions == null)
          {
              return Problem("Entity set 'BancobdContext.Transaccions'  is null.");
          }
            _context.Transaccions.Add(transaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaccion", new { id = transaccion.IdTransaccion }, transaccion);
        }

        // DELETE: api/Transaccions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccion(int id)
        {
            if (_context.Transaccions == null)
            {
                return NotFound();
            }
            var transaccion = await _context.Transaccions.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            _context.Transaccions.Remove(transaccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaccionExists(int id)
        {
            return (_context.Transaccions?.Any(e => e.IdTransaccion == id)).GetValueOrDefault();
        }
    }
}
