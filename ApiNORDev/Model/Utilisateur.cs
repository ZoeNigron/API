using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Utilisateur
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public string Prenom { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string MotDePasse { get; set; } = string.Empty;

        public int Score { get; set; } = 0;

        public Utilisateur() { }

        public Utilisateur(UtilisateurDTO dto)
        {
            Id = dto.Id;
            Nom = dto.Nom;
            Prenom = dto.Prenom;
            Email = dto.Email;
            MotDePasse = dto.MotDePasse ?? "";
            Score = dto.Score;
        }
    }
}
