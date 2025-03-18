using ApiNORDev.Data;
using ApiNORDev.Dto;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNORDev.Controllers
{
    [Route("api/astuces")]
    [ApiController]
    public class AstuceController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public AstuceController(ApiNORDevContext context)
        {
            _context = context;
        }

        // GET: api/astuce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AstuceDTO>>> GetAstuces()
        {
            var astuces = await _context
                .Astuces.Select(a => new AstuceDTO { Id = a.Id, Contenu = a.Contenu })
                .ToListAsync();

            return Ok(astuces);
        }

        // GET: api/astuce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AstuceDTO>> GetAstuce(int id)
        {
            var astuce = await _context
                .Astuces.Where(a => a.Id == id)
                .Select(a => new AstuceDTO { Id = a.Id, Contenu = a.Contenu })
                .FirstOrDefaultAsync();

            if (astuce == null)
            {
                return NotFound();
            }

            return Ok(astuce);
        }

        // POST: api/astuce
        [HttpPost]
        public async Task<ActionResult<AstuceDTO>> PostAstuce([FromBody] AstuceDTO astuceDTO)
        {
            if (astuceDTO == null || string.IsNullOrEmpty(astuceDTO.Contenu))
            {
                return BadRequest("Contenu de l'astuce est requis.");
            }

            var astuce = new Astuce { Contenu = astuceDTO.Contenu };

            _context.Astuces.Add(astuce);
            await _context.SaveChangesAsync();

            astuceDTO.Id = astuce.Id;
            return CreatedAtAction(nameof(GetAstuce), new { id = astuce.Id }, astuceDTO);
        }

        // PUT: api/astuce/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAstuce(int id, [FromBody] AstuceDTO astuceDTO)
        {
            if (id != astuceDTO.Id)
            {
                return BadRequest("L'ID de l'astuce ne correspond pas.");
            }

            var astuce = await _context.Astuces.FindAsync(id);

            if (astuce == null)
            {
                return NotFound();
            }

            astuce.Contenu = astuceDTO.Contenu;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Astuces.Any(a => a.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/astuce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAstuce(int id)
        {
            var astuce = await _context.Astuces.FindAsync(id);

            if (astuce == null)
            {
                return NotFound();
            }

            _context.Astuces.Remove(astuce);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
