using System.ComponentModel.DataAnnotations;

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
                if (value <= 0 || !int.TryParse(value.ToString(), out _id))
                {
                    throw new ArgumentException(
                        "L'identifiant de l'option doit être un entier positif"
                    );
                }
                _id = value;
            }
        }

        public string Texte { get; set; } = null!;

        public bool EstCorrecte { get; set; }

        public Option() { }

        public Option(OptionDTO optionDTO)
        {
            Id = optionDTO.Id;
            Texte = optionDTO.Texte;
            EstCorrecte = optionDTO.EstCorrecte;
        }
    }
}
