using Application.Interfaces;
using Domain.Entites;
using MassTransit;
using MediatR;
using SharedModelsLibrary;
using System;
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
            public PostWordCommandHandler(IWordsDbContext context, IPublishEndpoint publishEndpoint)
            {
                _context = context;
                _publishEndpoint = publishEndpoint;
            }
            private static DateTime DateToday
            {
                get { return DateTime.Today.ToUniversalTime(); }
            }
            public async Task<Word> Handle(PostWordCommand command, CancellationToken cancellationToken)
            {
                var word = command._word;

                var (longtitude, latitude) = MockLocation();

                word.AddTime = DateToday;
                word.LocationLongitude = longtitude;
                word.LocationLatitude = latitude;

                word.Text = word.Text.ToLower();
                word.Email = word.Email.ToLower();

                await PostAndPublishWord(word);

                return word;
            }
            private static (double longtitude, double latitude) MockLocation()
            {
                var longtitude = RandomCoordinate(180);
                var latitude = RandomCoordinate(90);

                var location = (longtitude, latitude);

                return location;
            }
            private static double RandomCoordinate(int degree)
            {
                var random = new Random();
                var intCoordinate = random.Next(-degree, degree);
                var doubleCoordinate = Math.Round(random.NextDouble(), 5);

                var coordinate = intCoordinate + doubleCoordinate;

                return coordinate;
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
