// Dans ce code, je crée une classe représentant une Astuce, qui est liée à son DTO AstuceDTO

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
