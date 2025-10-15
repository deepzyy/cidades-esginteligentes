using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Cap7.Data;
using WebService.Cap7.Model;

namespace WebService.Cap7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PontosDeDescarteController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PontosDeDescarteController(DatabaseContext context)
        {
            _context = (DatabaseContext)context;
        }

        // GET: api/pontos?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontoDeDescarte>>> GetAll(int page = 1, int pageSize = 10)
        {
            var pontos = await _context.PontosDeDescarte
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(pontos);
        }

        // GET: api/pontos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PontoDeDescarte>> GetById(int id)
        {
            var ponto = await _context.PontosDeDescarte.FindAsync(id);

            if (ponto == null)
                return NotFound();

            return Ok(ponto);
        }

        // POST: api/pontos
        [HttpPost]
        public async Task<ActionResult<PontoDeDescarte>> Create(PontoDeDescarte ponto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.PontosDeDescarte.Add(ponto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ponto.Id }, ponto);
        }

        // PUT: api/pontos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PontoDeDescarte ponto)
        {
            if (id != ponto.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            _context.Entry(ponto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PontosDeDescarte.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/pontos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ponto = await _context.PontosDeDescarte.FindAsync(id);
            if (ponto == null)
                return NotFound();

            _context.PontosDeDescarte.Remove(ponto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
