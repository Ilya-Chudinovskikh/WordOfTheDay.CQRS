using Application.Interfaces;
using Domain.Entites;
using MassTransit;
using MediatR;
using SharedModelsLibrary;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.WordFeatures.Commands
{
    public class PostWordCommand : IRequest<Word>
    {
        public Word _word;
        public PostWordCommand(Word word)
        {
            _word = word;
        }
        public class PostWordCommandHandler : IRequestHandler<PostWordCommand, Word>
        {
            private readonly IWordsDbContext _context;
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IMockLocation _mockLocation;
            private readonly IDateTodayService _dateToday;
            public PostWordCommandHandler(IWordsDbContext context, IPublishEndpoint publishEndpoint, IMockLocation mockLocation, IDateTodayService dateToday)
            {
                _context = context;
                _publishEndpoint = publishEndpoint;
                _mockLocation = mockLocation;
                _dateToday = dateToday;
            }
            public async Task<Word> Handle(PostWordCommand command, CancellationToken cancellationToken)
            {
                var word = command._word;
                var longtitude = _mockLocation.Longtitude;
                var latitude = _mockLocation.Latitude;

                word.AddTime = _dateToday.DateToday;
                word.LocationLongitude = longtitude;
                word.LocationLatitude = latitude;

                word.Text = word.Text.ToLower();
                word.Email = word.Email.ToLower();

                await PostAndPublishWord(word);

                return word;
            }
            private async Task PostAndPublishWord(Word word)
            {
                await PostWord(word);

                await _publishEndpoint.Publish(new WordInfo(word.Id, word.Email, word.Text, word.AddTime, word.LocationLongitude, word.LocationLatitude));
            }
            private async Task PostWord(Word word)
            {
                _context.Words.Add(word);
                await _context.SaveChanges();
            }
        }
    }
}
