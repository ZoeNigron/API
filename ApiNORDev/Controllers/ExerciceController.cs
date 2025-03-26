using System.Collections.Generic;
using System.Linq;
using ApiNORDev.Data;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiNORDev.Controllers
{
    [Route("api/exercices")]
    [ApiController]
    public class ExercicesController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public ExercicesController(ApiNORDevContext context)
        {
            _context = context;
        }

        // GET: api/exercices
        [HttpGet]
        public ActionResult<IEnumerable<Exercice>> GetExercices()
        {
            var exercices = _context.Exercices.ToList();
            if (!exercices.Any())
            {
                return NotFound("Aucun exercice trouvé.");
            }

            return Ok(exercices);
        }

        // GET: api/exercices/{id}
        [HttpGet("{id}")]
        public ActionResult<Exercice> GetExercice(int id)
        {
            var exercice = _context.Exercices.Find(id);

            if (exercice == null)
            {
                return NotFound($"L'exercice avec l'ID {id} n'existe pas.");
            }

            return Ok(exercice);
        }

        // POST: api/exercices
        [HttpPost]
        public ActionResult<Exercice> PostExercice([FromBody] Exercice exercice)
        {
            if (exercice == null)
            {
                return BadRequest("Les données de l'exercice sont invalides.");
            }

            _context.Exercices.Add(exercice);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetExercice), new { id = exercice.Id }, exercice);
        }

        // PUT: api/exercices/{id}
        [HttpPut("{id}")]
        public IActionResult PutExercice(int id, [FromBody] Exercice exercice)
        {
            if (id != exercice.Id)
            {
                return BadRequest("L'ID de l'exercice ne correspond pas.");
            }

            if (!_context.Exercices.Any(e => e.Id == id))
            {
                return NotFound($"L'exercice avec l'ID {id} n'existe pas.");
            }

            _context.Exercices.Update(exercice);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/exercices/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteExercice(int id)
        {
            var exercice = _context.Exercices.Find(id);

            if (exercice == null)
            {
                return NotFound($"L'exercice avec l'ID {id} n'existe pas.");
            }

            _context.Exercices.Remove(exercice);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
