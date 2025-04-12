// Dans ce code, je crée une classe représentant un Quiz, qui est liée à son DTO QuizDTO

using System.ComponentModel.DataAnnotations;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        public string? Titre { get; set; } = null!;

        public List<QuestionQuiz> QuestionsQuiz { get; set; } = new List<QuestionQuiz>();

        public Quiz() { }

        public Quiz(QuizDTO quizDTO)
        {
            Id = quizDTO.Id;
            Titre = quizDTO.Titre;
            QuestionsQuiz = quizDTO.QuestionsQuiz.Select(q => new QuestionQuiz(q)).ToList();
        }
    }
}
