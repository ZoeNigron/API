using System.ComponentModel.DataAnnotations;

namespace ApiNORDev.Model
{
    public class CompetenceDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Description { get; set; }
        public string? Lien { get; set; }

        public CompetenceDTO() { }

        public CompetenceDTO(Competence competence)
        {
            Id = competence.Id;
            Titre = competence.Titre;
            Description = competence.Description;
            Lien = competence.Lien;
        }
    }
}
