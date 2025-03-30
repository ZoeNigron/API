using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiNORDev.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Dto
{
    public class QuestionQuizDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; } = null!;

        [JsonPropertyName("explication")]
        public string Explication { get; set; } = null!;

        [JsonIgnore]
        [JsonPropertyName("options")]
        public List<OptionDTO> Options { get; set; } = new List<OptionDTO>();

        public QuestionQuizDTO() { }

        public QuestionQuizDTO(QuestionQuiz questionQuiz)
        {
            Id = questionQuiz.Id;
            Question = questionQuiz.Question;
            Explication = questionQuiz.Explication;
            // Convertir les options associées en OptionDTO
            Options = questionQuiz.Options.Select(o => new OptionDTO(o)).ToList();
        }
    }
}
