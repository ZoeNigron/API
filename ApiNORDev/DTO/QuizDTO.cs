using System.Collections.Generic;
using ApiNORDev.Model;

namespace ApiNORDev.Dto
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public List<QuestionQuizDTO> QuestionsQuiz { get; set; } = new List<QuestionQuizDTO>();

        public QuizDTO() { }

        // Constructeur qui prend le Quiz et ses questions par ID avec leurs options
        public QuizDTO(Quiz quiz)
        {
            Id = quiz.Id;
            Titre = quiz.Titre;

            // Remplir les questions et leurs options à partir de la liste de questions associées
            QuestionsQuiz = quiz.QuestionsQuiz.Select(q => new QuestionQuizDTO(q)).ToList();
        }
    }
}
