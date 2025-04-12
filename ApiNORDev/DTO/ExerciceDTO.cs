// Dans ce code, je crée un DTO à partir de l'objet Exercice du modèle de données

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class ExerciceDTO
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nom")]
        public string? Nom { get; set; }

        [JsonPropertyName("distanceCible")]
        public int DistanceCible { get; set; }

        public ExerciceDTO() { }

        public ExerciceDTO(Exercice exercice) // constructeur qui permet de transformer un objet Exercice en ExerciceDTO
        {
            Id = exercice.Id;
            Nom = exercice.Nom;
            DistanceCible = exercice.DistanceCible;
        }
    }
}
