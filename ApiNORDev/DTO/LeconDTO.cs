// Dans ce code, je crée un DTO à partir de l'objet Lecon du modèle de données

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class LeconDTO
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("titre")]
        public string? Titre { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("objectif")]
        public string? Objectif { get; set; }

        public LeconDTO() { }

        public LeconDTO(Lecon lecon) // constructeur qui permet de transformer un objet Lecon en LeconDTO
        {
            Id = lecon.Id;
            Titre = lecon.Titre;
            Description = lecon.Description;
            Objectif = lecon.Objectif;
        }
    }
}
