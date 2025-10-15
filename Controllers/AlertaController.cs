using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Cap7.Data;
using WebService.Cap7.Model;

namespace WebService.Cap7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AlertaController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/alerta?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alerta>>> GetAll(int page = 1, int pageSize = 10)
        {
            var alertas = await _context.Alertas
                .Include(a => a.Ponto)
                .OrderByDescending(a => a.DataCriacao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(alertas);
        }

        // GET: api/alertas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alerta>> GetById(int id)
        {
            var alerta = await _context.Alertas
                .Include(a => a.Ponto)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        // POST: api/alertas
        [HttpPost]
        public async Task<ActionResult<Alerta>> Create(Alerta alerta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            alerta.DataCriacao = DateTime.Now;

            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = alerta.Id }, alerta);
        }

        // PUT: api/alertas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Alerta alerta)
        {
            if (id != alerta.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            _context.Entry(alerta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alertas.Any(a => a.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/alertas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null)
                return NotFound();

            _context.Alertas.Remove(alerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
