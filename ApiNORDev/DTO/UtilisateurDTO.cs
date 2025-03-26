using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class UtilisateurDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nom")]
        public string Nom { get; set; } = string.Empty;

        [JsonPropertyName("prenom")]
        public string Prenom { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("motdepasse")]
        public string? MotDePasse { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        public UtilisateurDTO() { }

        public UtilisateurDTO(Utilisateur utilisateur)
        {
            Id = utilisateur.Id;
            Nom = utilisateur.Nom;
            Prenom = utilisateur.Prenom;
            Email = utilisateur.Email;
            MotDePasse = utilisateur.MotDePasse;
            Score = utilisateur.Score;
        }
    }
}
