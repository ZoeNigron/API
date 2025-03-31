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

        // Ajouter QuizId dans le DTO pour l'envoyer via l'API
        [JsonPropertyName("quizId")]
        public int QuizId { get; set; }

        // Liste des options associées à la question
        [JsonIgnore]
        [JsonPropertyName("options")]
        public List<OptionDTO> Options { get; set; } = new List<OptionDTO>();

        public QuestionQuizDTO() { }

        public QuestionQuizDTO(QuestionQuiz questionQuiz)
        {
            Id = questionQuiz.Id;
            Question = questionQuiz.Question;
            Explication = questionQuiz.Explication;
            QuizId = questionQuiz.QuizId;
            Options = questionQuiz.Options.Select(o => new OptionDTO(o)).ToList();
        }
    }
}
