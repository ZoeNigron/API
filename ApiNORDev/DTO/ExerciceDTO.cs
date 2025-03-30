using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class ExerciceDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Nom { get; set; }
        public int DistanceCible { get; set; }

        public ExerciceDTO() { }

        public ExerciceDTO(Exercice exercice)
        {
            Id = exercice.Id;
            Nom = exercice.Nom;
            DistanceCible = exercice.DistanceCible;
        }
    }
}
