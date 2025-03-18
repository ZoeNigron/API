using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class ReponseDTO
    {
        public int Id { get; set; }
        public string? Contenu { get; set; }

        public ReponseDTO(Reponse reponse)
        {
            Id = reponse.Id;
            Contenu = reponse.Contenu;
        }
    }
}
