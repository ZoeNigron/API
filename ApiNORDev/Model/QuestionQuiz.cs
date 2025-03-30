using System.Collections.Generic;
using ApiNORDev.Dto;

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
                // On s'assure que l'identifiant du client est un entier positif
                if (value <= 0 || !int.TryParse(value.ToString(), out _id))
                {
                    throw new ArgumentException(
                        "La valeur de l'identifiant doit être un entier positif"
                    );
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
