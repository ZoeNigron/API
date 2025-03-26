using System.Collections.Generic;
using System.Linq;
using ApiNORDev.Data;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiNORDev.Controllers
{
    [Route("api/competences")]
    [ApiController]
    public class CompetencesController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public CompetencesController(ApiNORDevContext context)
        {
            _context = context;
        }

        // GET: api/competences
        [HttpGet]
        public ActionResult<IEnumerable<Competence>> GetCompetences()
        {
            var competences = _context.Competences.ToList();
            if (!competences.Any())
            {
                return NotFound("Aucune compétence trouvée.");
            }

            return Ok(competences);
        }

        // GET: api/competences/{id}
        [HttpGet("{id}")]
        public ActionResult<Competence> GetCompetence(int id)
        {
            var competence = _context.Competences.Find(id);

            if (competence == null)
            {
                return NotFound($"La compétence avec l'ID {id} n'existe pas.");
            }

            return Ok(competence);
        }

        // POST: api/competences
        [HttpPost]
        public ActionResult<Competence> PostCompetence([FromBody] Competence competence)
        {
            if (competence == null)
            {
                return BadRequest("Les données de la compétence sont invalides.");
            }

            _context.Competences.Add(competence);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCompetence), new { id = competence.Id }, competence);
        }

        // PUT: api/competences/{id}
        [HttpPut("{id}")]
        public IActionResult PutCompetence(int id, [FromBody] Competence competence)
        {
            if (id != competence.Id)
            {
                return BadRequest("L'ID de la compétence ne correspond pas.");
            }

            if (!_context.Competences.Any(c => c.Id == id))
            {
                return NotFound($"La compétence avec l'ID {id} n'existe pas.");
            }

            _context.Competences.Update(competence);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/competences/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCompetence(int id)
        {
            var competence = _context.Competences.Find(id);

            if (competence == null)
            {
                return NotFound($"La compétence avec l'ID {id} n'existe pas.");
            }

            _context.Competences.Remove(competence);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
