using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class LeconDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Description { get; set; }
        public string? Objectif { get; set; }

        public LeconDTO() { }

        public LeconDTO(Lecon lecon)
        {
            Id = lecon.Id;
            Titre = lecon.Titre;
            Description = lecon.Description;
            Objectif = lecon.Objectif;
        }
    }
}
