// Dans ce code, je crée un DTO à partir de l'objet QuestionQuiz du modèle de données

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiNORDev.Model;

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

        [JsonPropertyName("quizId")]
        public int QuizId { get; set; }

        [JsonIgnore]
        [JsonPropertyName("options")]
        public List<OptionDTO> Options { get; set; } = new List<OptionDTO>();

        public QuestionQuizDTO() { }

        public QuestionQuizDTO(QuestionQuiz questionQuiz) // constructeur qui permet de transformer un objet QuestionQuiz en QuestionQuizDTO
        {
            Id = questionQuiz.Id;
            Question = questionQuiz.Question;
            Explication = questionQuiz.Explication;
            QuizId = questionQuiz.QuizId;
            Options = questionQuiz.Options.Select(o => new OptionDTO(o)).ToList();
        }
    }
}
