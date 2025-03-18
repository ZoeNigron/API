using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Contenu { get; set; }
        public int? IdReponseCorrecte { get; set; } // ID de la bonne réponse
        public List<ReponseDTO> Reponses { get; set; } = new List<ReponseDTO>();

        public QuestionDTO(Question question)
        {
            Id = question.Id;
            Contenu = question.Contenu;
            IdReponseCorrecte = question.IdReponseCorrecte;
            Reponses = question.Reponses.Select(r => new ReponseDTO(r)).ToList();
        }
    }
}
