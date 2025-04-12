// Dans ce code, je crée une classe représentant une Lecon, qui est liée à son DTO LeconDTO

using System.ComponentModel.DataAnnotations;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Lecon
    {
        [Key]
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Description { get; set; }
        public string? Objectif { get; set; }

        public Lecon() { }

        public Lecon(LeconDTO dto)
        {
            Id = dto.Id;
            Titre = dto.Titre;
            Description = dto.Description;
            Objectif = dto.Objectif;
        }
    }
}
