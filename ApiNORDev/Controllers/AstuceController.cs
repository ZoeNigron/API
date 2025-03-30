using ApiNORDev.Data;
using ApiNORDev.Dto;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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

        // GET: api/astuces
        [HttpGet]
        [SwaggerOperation(
            Summary = "Récupérer toutes les astuces",
            Description = "Récupère une liste de toutes les astuces."
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des astuces récupérée avec succès",
            typeof(IEnumerable<AstuceDTO>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucune astuce trouvée")]
        public async Task<ActionResult<IEnumerable<AstuceDTO>>> GetAstuces()
        {
            var astuces = await _context
                .Astuces.Select(a => new AstuceDTO { Id = a.Id, Contenu = a.Contenu })
                .ToListAsync();

            return Ok(astuces);
        }

        // GET: api/astuces/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupérer une astuce par ID",
            Description = "Récupère une astuce spécifique en fonction de son ID."
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Astuce récupérée avec succès",
            typeof(AstuceDTO)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Astuce introuvable")]
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

        // POST: api/astuces
        [HttpPost]
        [SwaggerOperation(
            Summary = "Créer une nouvelle astuce",
            Description = "Ajoute une nouvelle astuce à la base de données."
        )]
        [SwaggerResponse(
            StatusCodes.Status201Created,
            "Astuce créée avec succès",
            typeof(AstuceDTO)
        )]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Contenu de l'astuce est requis")]
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

        // PUT: api/astuces/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Mettre à jour une astuce",
            Description = "Met à jour une astuce existante avec un nouveau contenu."
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Astuce mise à jour avec succès")]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "L'ID de l'astuce ne correspond pas ou contenu manquant"
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Astuce introuvable")]
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

        // DELETE: api/astuces/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer une astuce",
            Description = "Supprime une astuce spécifique en fonction de son ID."
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Astuce supprimée avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Astuce introuvable")]
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
