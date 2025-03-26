using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiNORDev.Model
{
    public class OptionDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("texte")]
        public string Texte { get; set; } = null!;

        [JsonPropertyName("estCorrecte")]
        public bool EstCorrecte { get; set; }

        public OptionDTO() { }

        public OptionDTO(Option option)
        {
            Id = option.Id;
            Texte = option.Texte;
            EstCorrecte = option.EstCorrecte;
        }
    }
}
