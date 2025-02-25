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
        public string PasswordHash { get; set; } = string.Empty;

        public Utilisateur() { }

        public Utilisateur(UtilisateurDTO dto)
        {
            Id = dto.Id;
            Nom = dto.Nom;
            Prenom = dto.Prenom;
            Email = dto.Email;
            PasswordHash = HashPassword(dto.MotDePasse ?? ""); // Hachage sécurisé
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
