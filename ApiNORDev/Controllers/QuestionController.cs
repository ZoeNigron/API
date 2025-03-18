using ApiNORDev.Dto;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public QuestionController(ApiNORDevContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions()
        {
            var questions = await _context.Questions.Include(q => q.Reponses).ToListAsync();
            var questionDtos = questions.Select(q => new QuestionDTO(q)).ToList();
            return Ok(questionDtos);
        }
    }
}
