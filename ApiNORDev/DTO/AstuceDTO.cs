// Dans ce code, je crée un DTO à partir de l'objet Astuce du modèle de données

using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class AstuceDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("contenu")]
        public string? Contenu { get; set; }

        public AstuceDTO() { }

        public AstuceDTO(Astuce astuce) // constructeur qui permet de transformer un objet Astuce en AstuceDTO
        {
            Id = astuce.Id;
            Contenu = astuce.Contenu;
        }
    }
}
