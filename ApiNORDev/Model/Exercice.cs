using System.ComponentModel.DataAnnotations;

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
