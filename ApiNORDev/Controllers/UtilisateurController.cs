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
        [SwaggerOperation(
            Summary = "Liste de tous les utilisateurs",
            Description = "Récupère tous les utilisateurs"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des utilisateurs trouvée",
            typeof(IEnumerable<UtilisateurDTO>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucun utilisateur trouvé")]
        public async Task<ActionResult<IEnumerable<UtilisateurDTO>>> GetUtilisateurs()
        {
            var utilisateurs = await _context
                .Utilisateurs.Select(u => new UtilisateurDTO(u))
                .ToListAsync();
            return utilisateurs.Any() ? Ok(utilisateurs) : NotFound();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupérer un utilisateur par ID",
            Description = "Renvoie un utilisateur spécifique"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Utilisateur trouvé", typeof(UtilisateurDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Utilisateur introuvable")]
        public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            return utilisateur != null ? Ok(new UtilisateurDTO(utilisateur)) : NotFound();
        }

        [HttpPost("creer")]
        [SwaggerOperation(
            Summary = "Créer un utilisateur",
            Description = "Ajoute un nouvel utilisateur"
        )]
        [SwaggerResponse(
            StatusCodes.Status201Created,
            "Utilisateur créé avec succès",
            typeof(UtilisateurDTO)
        )]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Données invalides ou email déjà utilisé"
        )]
        public async Task<IActionResult> PostUtilisateur([FromBody] UtilisateurDTO utilisateurDto)
        {
            Console.WriteLine(
                $"Requête reçue : {utilisateurDto?.Nom}, {utilisateurDto?.Email}, {utilisateurDto?.MotDePasse}"
            );

            if (utilisateurDto == null || string.IsNullOrEmpty(utilisateurDto.MotDePasse))
            {
                return BadRequest("Les informations de l'utilisateur sont incomplètes.");
            }

            var emailExiste = await _context.Utilisateurs.AnyAsync(u =>
                u.Email == utilisateurDto.Email
            );
            if (emailExiste)
            {
                return BadRequest(new { message = "Cet email est déjà utilisé." });
            }

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
        [SwaggerOperation(
            Summary = "Connexion utilisateur",
            Description = "Permet à un utilisateur de se connecter"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Connexion réussie", typeof(object))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Email ou mot de passe requis")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Email ou mot de passe incorrect")]
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
        [SwaggerOperation(
            Summary = "Mettre à jour un utilisateur",
            Description = "Modifie un utilisateur existant"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Utilisateur mis à jour avec succès",
            typeof(UtilisateurDTO)
        )]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Données invalides")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Utilisateur introuvable")]
        public async Task<IActionResult> PutUtilisateur(
            int id,
            [FromBody] UtilisateurDTO utilisateurDto
        )
        {
            if (
                utilisateurDto == null
                || string.IsNullOrEmpty(utilisateurDto.Nom)
                || string.IsNullOrEmpty(utilisateurDto.Email)
            )
            {
                return BadRequest("Les informations de l'utilisateur sont incomplètes.");
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound("L'utilisateur avec l'ID donné n'existe pas.");
            }

            utilisateur.Nom = utilisateurDto.Nom;
            utilisateur.Prenom = utilisateurDto.Prenom;
            utilisateur.Email = utilisateurDto.Email;

            if (!string.IsNullOrEmpty(utilisateurDto.MotDePasse))
            {
                utilisateur.MotDePasse = utilisateurDto.MotDePasse;
            }

            _context.Entry(utilisateur).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new UtilisateurDTO(utilisateur));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer un utilisateur",
            Description = "Supprime un utilisateur existant"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Utilisateur supprimé avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Utilisateur introuvable")]
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
        [SwaggerOperation(
            Summary = "Mettre à jour le score d'un utilisateur",
            Description = "Ajoute un score à un utilisateur"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Score mis à jour avec succès",
            typeof(Utilisateur)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Utilisateur introuvable")]
        public async Task<IActionResult> UpdateScore(int userId, int score)
        {
            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Id == userId);
            if (utilisateur == null)
            {
                return NotFound("Utilisateur non trouvé");
            }

            utilisateur.Score += score;
            _context.SaveChanges();

            return Ok(utilisateur);
        }

        [HttpGet("topScores")]
        [SwaggerOperation(
            Summary = "Obtenir les meilleurs scores",
            Description = "Récupère les 10 meilleurs scores des utilisateurs"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Top scores récupérés avec succès",
            typeof(IEnumerable<Utilisateur>)
        )]
        public IActionResult GetTopScores()
        {
            var topScores = _context.Utilisateurs.OrderByDescending(u => u.Score).Take(10).ToList();
            return Ok(topScores);
        }
    }
}
