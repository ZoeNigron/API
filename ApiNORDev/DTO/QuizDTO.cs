using System.Collections.Generic;
using System.Linq;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public List<QuestionQuizDTO>? QuestionsQuiz { get; set; } = new List<QuestionQuizDTO>();

        public QuizDTO() { }

        // Constructeur qui prend le Quiz et ses questions par ID
        public QuizDTO(Quiz quiz, List<QuestionQuiz> questions)
        {
            Id = quiz.Id;
            Titre = quiz.Titre;
            // Remplir les questions à partir des QuestionQuizIds
            QuestionsQuiz = questions
                .Where(q => quiz.QuestionQuizIds.Contains(q.Id))
                .Select(q => new QuestionQuizDTO(q))
                .ToList();
        }
    }
}
