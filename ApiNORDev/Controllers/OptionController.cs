using ApiNORDev.Data;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [ApiController]
    [Route("api/options")]
    public class OptionController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public OptionController(ApiNORDevContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste de toutes les options",
            Description = "Récupère les informations détaillées de toutes les options enregistrées"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des options trouvée",
            typeof(IEnumerable<OptionDTO>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucune option trouvée")]
        public async Task<ActionResult<IEnumerable<OptionDTO>>> GetOptions()
        {
            var options = await _context.Options
                .Select(o => new OptionDTO
                {
                    Id = o.Id,
                    Texte = o.Texte,
                    EstCorrecte = o.EstCorrecte
                }).ToListAsync();

            if (!options.Any())
            {
                return NotFound();
            }

            return Ok(options);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Liste d'une option unique",
            Description = "Récupère les informations détaillées d'une option par son identifiant"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Option trouvée", typeof(OptionDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Option introuvable")]
        public async Task<ActionResult<OptionDTO>> GetOption(int id)
        {
            var option = await _context.Options
                .SingleOrDefaultAsync(o => o.Id == id);

            if (option == null)
            {
                return NotFound();
            }

            return Ok(new OptionDTO
            {
                Id = option.Id,
                Texte = option.Texte,
                EstCorrecte = option.EstCorrecte
            });
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Ajouter une option",
            Description = "Ajoute une nouvelle option à la base de données"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Option ajoutée avec succès", typeof(OptionDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Impossible d'ajouter l'option")]
        public async Task<ActionResult<OptionDTO>> PostOption([FromBody] OptionDTO optionDTO)
        {
            if (optionDTO == null)
            {
                return NotFound();
            }

            var option = new Option
            {
                Texte = optionDTO.Texte,
                EstCorrecte = optionDTO.EstCorrecte
            };

            try
            {
                _context.Options.Add(option);
                await _context.SaveChangesAsync();

                return Ok(new OptionDTO
                {
                    Id = option.Id,
                    Texte = option.Texte,
                    EstCorrecte = option.EstCorrecte
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Mettre à jour une option",
            Description = "Met à jour les informations d'une option existante en fonction de son identifiant"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Option mise à jour avec succès",
            typeof(OptionDTO)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Option introuvable ou non mise à jour")]
        public async Task<IActionResult> PutOption(int id, [FromBody] OptionDTO optionDTO)
        {
            if (id != optionDTO.Id)
            {
                return NotFound();
            }

            var optionExistante = await _context.Options.FindAsync(id);
            if (optionExistante == null)
            {
                return NotFound();
            }

            optionExistante.Texte = optionDTO.Texte;
            optionExistante.EstCorrecte = optionDTO.EstCorrecte;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Options.Any(o => o.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(optionDTO);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer une option",
            Description = "Supprime une option existante en fonction de son identifiant"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Option supprimée avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Option introuvable")]
        public async Task<ActionResult> DeleteOption(int id)
        {
            var option = await _context.Options.FindAsync(id);

            if (option == null)
            {
                return NotFound();
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Option supprimée avec succès" });
        }
    }
}
