using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Reponse
    {
        public int Id { get; set; }
        public string? Contenu { get; set; }
        public int QuestionId { get; set; }
        public bool EstCorrecte { get; set; }

        public Reponse() { }

        public Reponse(ReponseDTO dto)
        {
            Id = dto.Id;
            Contenu = dto.Contenu;
        }
    }
}
