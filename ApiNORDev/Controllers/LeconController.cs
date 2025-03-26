using System.Collections.Generic;
using System.Linq;
using ApiNORDev.Data;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/lecons
        [HttpGet]
        public ActionResult<IEnumerable<Lecon>> GetLecons()
        {
            var lecons = _context.Lecons.ToList();
            if (!lecons.Any())
            {
                return NotFound("Aucune leçon trouvée.");
            }

            return Ok(lecons);
        }

        // GET: api/lecons/{id}
        [HttpGet("{id}")]
        public ActionResult<Lecon> GetLecon(int id)
        {
            var lecon = _context.Lecons.Find(id);

            if (lecon == null)
            {
                return NotFound($"La leçon avec l'ID {id} n'existe pas.");
            }

            return Ok(lecon);
        }

        // POST: api/lecons
        [HttpPost]
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

        // PUT: api/lecons/{id}
        [HttpPut("{id}")]
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

        // DELETE: api/lecons/{id}
        [HttpDelete("{id}")]
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
