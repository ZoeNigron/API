using System.Security.Cryptography;
using System.Text;
using ApiNORDev.Dto;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [ApiController]
    [Route("api/utilisateur")]
    public class UtilisateurController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public UtilisateurController(ApiNORDevContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateurDTO>>> GetUtilisateurs()
        {
            var Utilisateurs = await _context
                .Utilisateurs.Select(u => new UtilisateurDTO(u))
                .ToListAsync();
            return Utilisateurs.Any() ? Ok(Utilisateurs) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(int id)
        {
            var Utilisateur = await _context.Utilisateurs.FindAsync(id);
            return Utilisateur != null ? Ok(new UtilisateurDTO(Utilisateur)) : NotFound();
        }

        // Méthode pour ajouter un utilisateur
        [HttpPost]
        public async Task<IActionResult> PostUtilisateur(UtilisateurDTO utilisateurDto)
        {
            if (utilisateurDto == null)
            {
                return BadRequest("UtilisateurDTO est nul");
            }

            // Mapper UtilisateurDTO vers un modèle Utilisateur
            var utilisateur = new Utilisateur
            {
                Nom = utilisateurDto.Nom,
                Prenom = utilisateurDto.Prenom,
                Email = utilisateurDto.Email,
                PasswordHash = "MotDePasseParDefaut", // Tu peux ajouter un mot de passe par défaut ou gérer l'inscription
            };

            // Ajouter l'utilisateur à la base de données
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUtilisateur),
                new { id = utilisateur.Id },
                utilisateur
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var Utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (Utilisateur == null)
                return NotFound();

            _context.Utilisateurs.Remove(Utilisateur);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
