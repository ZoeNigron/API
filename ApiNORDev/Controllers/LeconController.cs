// Le contrôleur LeconController gère les opérations CRUD pour les lecons, telles que la récupération, la création, la mise à jour et la suppression de lecons

using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [Route("api/lecons")]
    [ApiController]
    public class LeconsController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public LeconsController(ApiNORDevContext context)
        {
            _context = context;
        }

        // récupérer toutes les leçons
        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste de toutes les leçons",
            Description = "Récupère toutes les leçons disponibles"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des leçons trouvée",
            typeof(IEnumerable<Lecon>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucune leçon trouvée")]
        public ActionResult<IEnumerable<Lecon>> GetLecons()
        {
            var lecons = _context.Lecons.ToList();
            if (!lecons.Any())
            {
                return NotFound("Aucune leçon trouvée.");
            }

            return Ok(lecons);
        }

        // récupérer une leçon par son id
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupérer une leçon par ID",
            Description = "Récupère une leçon spécifique à partir de son ID"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Leçon trouvée", typeof(Lecon))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Leçon introuvable")]
        public ActionResult<Lecon> GetLecon(int id)
        {
            var lecon = _context.Lecons.Find(id);

            if (lecon == null)
            {
                return NotFound($"La leçon avec l'ID {id} n'existe pas.");
            }

            return Ok(lecon);
        }

        // créer une nouvelle leçon
        [HttpPost]
        [SwaggerOperation(
            Summary = "Créer une nouvelle leçon",
            Description = "Ajoute une nouvelle leçon"
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "Leçon créée avec succès", typeof(Lecon))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Les données de la leçon sont invalides")]
        public ActionResult<Lecon> PostLecon([FromBody] Lecon lecon)
        {
            if (lecon == null)
            {
                return BadRequest("Les données de la leçon sont invalides.");
            }

            _context.Lecons.Add(lecon);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLecon), new { id = lecon.Id }, lecon);
        }

        // mettre à jour une leçon existante
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Mettre à jour une leçon",
            Description = "Met à jour une leçon existante"
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Leçon mise à jour avec succès")]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Les données de la leçon sont invalides ou l'ID ne correspond pas"
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Leçon introuvable")]
        public IActionResult PutLecon(int id, [FromBody] Lecon lecon)
        {
            if (id != lecon.Id)
            {
                return BadRequest("L'ID de la leçon ne correspond pas.");
            }

            if (!_context.Lecons.Any(l => l.Id == id))
            {
                return NotFound($"La leçon avec l'ID {id} n'existe pas.");
            }

            _context.Lecons.Update(lecon);
            _context.SaveChanges();

            return NoContent();
        }

        // supprimer une leçon par son id
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer une leçon",
            Description = "Supprime une leçon existante à partir de son ID"
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Leçon supprimée avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Leçon introuvable")]
        public IActionResult DeleteLecon(int id)
        {
            var lecon = _context.Lecons.Find(id);

            if (lecon == null)
            {
                return NotFound($"La leçon avec l'ID {id} n'existe pas.");
            }

            _context.Lecons.Remove(lecon);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
