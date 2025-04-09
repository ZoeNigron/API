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
        public string? Icone { get; set; }
        public string? CategorieIcone { get; set; }

        public Competence() { }

        public Competence(CompetenceDTO dto)
        {
            Id = dto.Id;
            Titre = dto.Titre;
            Description = dto.Description;
            Lien = dto.Lien;
            Icone = dto.Icone;
            CategorieIcone = dto.CategorieIcone;
        }
    }
}
