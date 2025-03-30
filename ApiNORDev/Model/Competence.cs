using System.ComponentModel.DataAnnotations;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Competence
    {
        [Key]
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Description { get; set; }
        public string? Lien { get; set; }

        // mettre image

        public Competence() { }

        public Competence(CompetenceDTO dto)
        {
            Id = dto.Id;
            Titre = dto.Titre;
            Description = dto.Description;
            Lien = dto.Lien;
        }
    }
}
