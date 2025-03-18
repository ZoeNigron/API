using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = "";
        public ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();
        public int? IdReponseCorrecte { get; set; } // ID de la bonne réponse

        public Question() { }

        public Question(QuestionDTO dto)
        {
            Id = dto.Id;
            Contenu = dto.Contenu;
            Reponses = dto
                .Reponses.Select(r => new Reponse
                {
                    Id = r.Id,
                    Contenu = r.Contenu,
                    EstCorrecte = dto.IdReponseCorrecte == r.Id, // Marque la bonne réponse
                })
                .ToList();
            IdReponseCorrecte = dto.IdReponseCorrecte; // Enregistrer l'ID de la bonne réponse
        }
    }
}
