// Dans ce code, je crée une classe représentant une QuestionQuiz, qui est liée à son DTO QuestionQuizDTO

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
                // S'assurer que l'identifiant est un entier positif
                if (value <= 0 || !int.TryParse(value.ToString(), out _id))
                {
                    throw new ArgumentException("L'identifiant doit être un entier positif");
                }
                _id = value;
            }
        }

        public string Question { get; set; } = null!;
        public string Explication { get; set; } = null!;

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; } = null!;

        public List<Option> Options { get; set; } = new List<Option>();

        public QuestionQuiz() { }

        public QuestionQuiz(QuestionQuizDTO questionQuizDTO)
        {
            Id = questionQuizDTO.Id;
            Question = questionQuizDTO.Question;
            Explication = questionQuizDTO.Explication;
            QuizId = questionQuizDTO.QuizId;
        }
    }
}
