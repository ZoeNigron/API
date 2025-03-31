using ApiNORDev.Dto;

namespace ApiNORDev.Model
{
    public class Option
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

        public string Texte { get; set; } = null!;

        public bool EstCorrecte { get; set; }

        public int QuestionQuizId { get; set; }

        public QuestionQuiz QuestionQuiz { get; set; } = null!;

        public Option() { }

        public Option(OptionDTO optionDTO)
        {
            QuestionQuizId = optionDTO.QuestionQuizId;
            Id = optionDTO.Id;
            Texte = optionDTO.Texte;
            EstCorrecte = optionDTO.EstCorrecte;
        }
    }
}
