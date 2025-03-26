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
            var utilisateurs = await _context
                .Utilisateurs.Select(u => new UtilisateurDTO(u))
                .ToListAsync();
            return utilisateurs.Any() ? Ok(utilisateurs) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            return utilisateur != null ? Ok(new UtilisateurDTO(utilisateur)) : NotFound();
        }

        [HttpPost("creer")]
        public async Task<IActionResult> PostUtilisateur([FromBody] UtilisateurDTO utilisateurDto)
        {
            Console.WriteLine(
                $"Requête reçue : {utilisateurDto?.Nom}, {utilisateurDto?.Email}, {utilisateurDto?.MotDePasse}"
            );

            if (utilisateurDto == null || string.IsNullOrEmpty(utilisateurDto.MotDePasse))
            {
                return BadRequest("Les informations de l'utilisateur sont incomplètes.");
            }

            // Vérifie si l'email existe déjà dans la base de données
            var emailExiste = await _context.Utilisateurs.AnyAsync(u =>
                u.Email == utilisateurDto.Email
            );
            if (emailExiste)
            {
                // Si l'email existe déjà, renvoie une erreur 400
                return BadRequest(new { message = "Cet email est déjà utilisé." });
            }

            // Si l'email n'existe pas, crée un nouvel utilisateur
            var utilisateur = new Utilisateur
            {
                Nom = utilisateurDto.Nom,
                Prenom = utilisateurDto.Prenom,
                Email = utilisateurDto.Email,
                MotDePasse = utilisateurDto.MotDePasse,
            };

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUtilisateur),
                new { id = utilisateur.Id },
                new UtilisateurDTO(utilisateur)
            );
        }

        [HttpPost("connecter")]
        public async Task<IActionResult> Login([FromBody] UtilisateurDTO utilisateurDto)
        {
            if (
                string.IsNullOrEmpty(utilisateurDto.Email)
                || string.IsNullOrEmpty(utilisateurDto.MotDePasse)
            )
            {
                return BadRequest("Email et mot de passe requis.");
            }

            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u =>
                u.Email == utilisateurDto.Email
            );

            if (utilisateur == null || utilisateur.MotDePasse != utilisateurDto.MotDePasse)
            {
                return Unauthorized("Email ou mot de passe incorrect.");
            }

            return Ok(
                new
                {
                    message = "Connexion réussie",
                    token = "exempleToken",
                    id = utilisateur.Id,
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(
            int id,
            [FromBody] UtilisateurDTO utilisateurDto
        )
        {
            // Vérifie si les données envoyées sont valides
            if (
                utilisateurDto == null
                || string.IsNullOrEmpty(utilisateurDto.Nom)
                || string.IsNullOrEmpty(utilisateurDto.Email)
            )
            {
                return BadRequest("Les informations de l'utilisateur sont incomplètes.");
            }

            // Recherche l'utilisateur à mettre à jour
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound("L'utilisateur avec l'ID donné n'existe pas.");
            }

            // Mise à jour des informations de l'utilisateur
            utilisateur.Nom = utilisateurDto.Nom;
            utilisateur.Prenom = utilisateurDto.Prenom;
            utilisateur.Email = utilisateurDto.Email;

            // Si un nouveau mot de passe est fourni, on le met à jour
            if (!string.IsNullOrEmpty(utilisateurDto.MotDePasse))
            {
                utilisateur.MotDePasse = utilisateurDto.MotDePasse;
            }

            // Sauvegarde les changements dans la base de données
            _context.Entry(utilisateur).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Retourne les informations de l'utilisateur mises à jour
            return Ok(new UtilisateurDTO(utilisateur));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
                return NotFound();

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("updateScore")]
        public async Task<IActionResult> UpdateScore(int userId, int score)
        {
            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Id == userId);
            if (utilisateur == null)
            {
                return NotFound("Utilisateur non trouvé");
            }

            // Ajout de la logique pour valider le score consécutif si nécessaire
            utilisateur.Score += score; // Ajouter ou mettre à jour le score
            _context.SaveChanges();

            return Ok(utilisateur);
        }

        [HttpGet("topScores")]
        public IActionResult GetTopScores()
        {
            var topScores = _context.Utilisateurs.OrderByDescending(u => u.Score).Take(10).ToList();

            return Ok(topScores);
        }
    }
}
