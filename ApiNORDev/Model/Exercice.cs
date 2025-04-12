// Dans ce code, je crée une classe représentant un Exercice, qui est liée à son DTO ExerciceDTO

using System.ComponentModel.DataAnnotations;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Exercice
    {
        [Key]
        public int Id { get; set; }
        public string? Nom { get; set; }
        public int DistanceCible { get; set; }

        public Exercice() { }

        public Exercice(ExerciceDTO dto)
        {
            Id = dto.Id;
            Nom = dto.Nom;
            DistanceCible = dto.DistanceCible;
        }
    }
}
