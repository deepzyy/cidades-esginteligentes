using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Cap7.Data;
using WebService.Cap7.Model;

namespace WebService.Cap7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColetaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ColetaController(DbContext context)
        {
            _context = (DatabaseContext)context;
        }

        // GET: api/coletas?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coleta>>> GetAll(int page = 1, int pageSize = 10)
        {
            var coletas = await _context.Coletas
                .Include(c => c.Residuo)
                .Include(c => c.Ponto)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(coletas);
        }

        // GET: api/coletas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coleta>> GetById(int id)
        {
            var coleta = await _context.Coletas
                .Include(c => c.Residuo)
                .Include(c => c.Ponto)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coleta == null)
                return NotFound();

            return Ok(coleta);
        }

        // POST: api/coletas
        [HttpPost]
        public async Task<ActionResult<Coleta>> Create(Coleta coleta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ponto = await _context.PontosDeDescarte.FindAsync(coleta.PontoDeDescarteId);
            if (ponto == null)
                return NotFound("Ponto de descarte não encontrado.");

            // Atualiza a quantidade atual de resíduos no ponto
            ponto.QuantidadeAtualKg += coleta.Quantidade;

            // Verifica se ultrapassou a capacidade máxima
            if (ponto.QuantidadeAtualKg > ponto.CapacidadeMaximaKg)
            {
                var alerta = new Alerta
                {
                    Mensagem = $" Limite de capacidade excedido no ponto '{ponto.Endereco}'.",
                    DataCriacao = DateTime.Now,
                    PontoDeDescarteId = ponto.Id
                };

                _context.Alertas.Add(alerta);
            }

            _context.Coletas.Add(coleta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = coleta.Id }, coleta);
        }

        // PUT: api/coletas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Coleta coleta)
        {
            if (id != coleta.Id)
                return BadRequest("ID da URL não corresponde ao corpo da requisição.");

            _context.Entry(coleta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Coletas.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/coletas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var coleta = await _context.Coletas.FindAsync(id);
            if (coleta == null)
                return NotFound();

            _context.Coletas.Remove(coleta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
