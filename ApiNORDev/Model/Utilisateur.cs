using System.ComponentModel.DataAnnotations;
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

        public List<int> LeconsValidees { get; set; } = new List<int>();

        public Utilisateur() { }

        public Utilisateur(UtilisateurDTO dto)
        {
            Id = dto.Id;
            Nom = dto.Nom;
            Prenom = dto.Prenom;
            Email = dto.Email;
            MotDePasse = dto.MotDePasse ?? "";
        }
    }
}
