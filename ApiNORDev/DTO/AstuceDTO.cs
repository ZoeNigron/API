using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class AstuceDTO
    {
        public int Id { get; set; }
        public string? Contenu { get; set; }

        public AstuceDTO() { }

        public AstuceDTO(int id, string contenu)
        {
            Id = id;
            Contenu = contenu;
        }
    }
}
