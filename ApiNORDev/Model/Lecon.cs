using System.ComponentModel.DataAnnotations;

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
