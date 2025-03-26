using System.Collections.Generic;

namespace ApiNORDev.Model
{
    public class QuestionQuiz
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0 || !int.TryParse(value.ToString(), out _id))
                {
                    throw new ArgumentException("L'identifiant doit être un entier positif");
                }
                _id = value;
            }
        }

        public string Question { get; set; } = null!;

        public List<Option> Options { get; set; } = new();

        public string Explication { get; set; } = null!;

        public QuestionQuiz() { }

        public QuestionQuiz(QuestionQuizDTO questionQuizDTO)
        {
            Id = questionQuizDTO.Id;
            Question = questionQuizDTO.Question;
            Explication = questionQuizDTO.Explication;
        }
    }
}
