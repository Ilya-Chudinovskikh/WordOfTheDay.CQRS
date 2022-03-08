using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries;
using Application.Features.WordFeatures.Commands;
using Domain.Entites;

namespace WebApi.Controllers
{
    [Route("api/words")]
    [ApiController]
    public class WordsController : Controller
    {
        private readonly IMediator _mediator;
        public WordsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get-word-of-the-day")]
        public async Task<IActionResult> GetWordOfTheDay()
        {
            var wordOfTheDay = await _mediator.Send(new GetWordOfTheDayQuery());

            if (wordOfTheDay == null)
            {
                return NotFound();
            }

            return Ok(wordOfTheDay);
        }
        [HttpGet("get-closest-words/{text}")]
        public async Task<IActionResult> GetClosestWords(string text)
        {
            var closestWords = await _mediator.Send(new GetClosestWordsQuery(text));

            return Ok(closestWords);
        }
        [HttpGet("{text}")]
        public async Task<IActionResult> GetUserWord(string text)
        {
            var userWord = await _mediator.Send(new GetUserWordQuery(text));

            return Ok(userWord);
        }
        [HttpPost]
        public async Task<IActionResult> PostWord(Word word)
        {
            if (word == null)
            {
                return BadRequest();
            }

            if (await _mediator.Send(new EmailIsAlreadyUsedQuery(word.Email)))
                ModelState.AddModelError("Email", "Users with the same email address can add only one word per day!");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(new PostWordCommand(word));

            return Ok(word);
        }
    }
}
