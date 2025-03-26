using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiNORDev.Model
{
    public class QuestionQuizDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; } = null!;

        [JsonPropertyName("options")]
        public List<OptionDTO> Options { get; set; } = new();

        [JsonPropertyName("explication")]
        public string Explication { get; set; } = null!;

        public QuestionQuizDTO() { }

        public QuestionQuizDTO(QuestionQuiz questionQuiz)
        {
            Id = questionQuiz.Id;
            Question = questionQuiz.Question;
            Explication = questionQuiz.Explication;
            Options = new List<OptionDTO>();
            foreach (var option in questionQuiz.Options)
            {
                Options.Add(new OptionDTO(option));
            }
        }
    }
}
