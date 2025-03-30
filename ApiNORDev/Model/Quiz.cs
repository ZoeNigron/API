using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        public string Titre { get; set; } = null!;

        // Liste des IDs des questions associées au quiz
        public List<int> QuestionQuizIds { get; set; } = new List<int>();

        public Quiz() { }

        public Quiz(QuizDTO quizDTO)
        {
            Id = quizDTO.Id;
            Titre = quizDTO.Titre;
            QuestionQuizIds = quizDTO.QuestionsQuiz.Select(q => q.Id).ToList();
        }
    }
}
