using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Cap7.Data;
using WebService.Cap7.Model;


namespace WebService.Cap7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResiduoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ResiduoController(DbContext context)
        {
            _context = (DatabaseContext)context;
        }

        // GET: api/residuo?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Residuo>>> GetAll(int page = 1, int pageSize = 10)
        {
            var residuos = await _context.Residuos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(residuos);
        }

        // GET: api/residuo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Residuo>> GetById(int id)
        {
            var residuo = await _context.Residuos.FindAsync(id);

            if (residuo == null)
                return NotFound();

            return Ok(residuo);
        }

        // POST: api/residuo
        [HttpPost]
        public async Task<ActionResult<Residuo>> Create([FromBody] Residuo residuo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Residuos.Add(residuo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = residuo.Id }, residuo);
        }

        // PUT: api/residuo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Residuo residuo)
        {
            if (id != residuo.Id)
                return BadRequest("ID da URL não corresponde ao objeto enviado.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(residuo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Residuos.Any(r => r.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/residuo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var residuo = await _context.Residuos.FindAsync(id);
            if (residuo == null)
                return NotFound();

            _context.Residuos.Remove(residuo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
