// Dans ce code, je crée un DTO à partir de l'objet Quiz du modèle de données

using System.Text.Json.Serialization;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class QuizDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("titre")]
        public string? Titre { get; set; }

        [JsonPropertyName("questionsQuiz")]
        public List<QuestionQuizDTO> QuestionsQuiz { get; set; } = new List<QuestionQuizDTO>();

        public QuizDTO() { }

        public QuizDTO(Quiz quiz) // constructeur qui permet de transformer un objet Quiz en QuizDTO
        {
            Id = quiz.Id;
            Titre = quiz.Titre;

            // on ajoute les questions et leurs options à partir de la liste de questions associées
            QuestionsQuiz = quiz.QuestionsQuiz.Select(q => new QuestionQuizDTO(q)).ToList();
        }
    }
}
