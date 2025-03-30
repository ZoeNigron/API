using System.Collections.Generic;
using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class OptionDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("texte")]
        public string Texte { get; set; } = null!;

        [JsonPropertyName("estCorrecte")]
        public bool EstCorrecte { get; set; }

        [JsonPropertyName("questionQuizId")]
        public int QuestionQuizId { get; set; }

        public OptionDTO() { }

        public OptionDTO(Option option)
        {
            QuestionQuizId = option.QuestionQuizId;
            Id = option.Id;
            Texte = option.Texte;
            EstCorrecte = option.EstCorrecte;
        }
    }
}
