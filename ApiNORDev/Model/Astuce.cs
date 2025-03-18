using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Astuce
    {
        public int Id { get; set; }
        public string? Contenu { get; set; } = "";

        public Astuce() { }

        public Astuce(AstuceDTO dto)
        {
            Id = dto.Id;
            Contenu = dto.Contenu;
        }
    }
}
