// Dans ce code, je crée un DTO à partir de l'objet Astuce du modèle de données.
// Le but est de ne garder que les infos nécessaires (ici : id et contenu) pour les envoyer plus facilement en JSON, dans mon API REST. Cela évite d’exposer toute la structure interne de l’objet Astuce et permet aussi de mieux contrôler ce qui est envoyé ou reçu côté client

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

        public AstuceDTO(Astuce astuce)
        {
            Id = astuce.Id;
            Contenu = astuce.Contenu;
        }
    }
}
