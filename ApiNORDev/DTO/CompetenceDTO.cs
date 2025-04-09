using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class CompetenceDTO
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("titre")]
        public string? Titre { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("lien")]
        public string? Lien { get; set; }

        [JsonPropertyName("icone")]
        public string? Icone { get; set; }

        [JsonPropertyName("categorieIcone")]
        public string? CategorieIcone { get; set; }

        public CompetenceDTO() { }

        public CompetenceDTO(Competence competence)
        {
            Id = competence.Id;
            Titre = competence.Titre;
            Description = competence.Description;
            Lien = competence.Lien;
            Icone = competence.Icone;
            CategorieIcone = competence?.CategorieIcone;
        }
    }
}
