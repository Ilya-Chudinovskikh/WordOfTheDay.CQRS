using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;
using MongoDB.Driver;

namespace Application.Features.WordFeatures.Queries
{
    public class GetWordOfTheDayQuery : IRequest<WordCount>
    {
        public GetWordOfTheDayQuery()
        {
        }
        public class GetWordOfTheDayQueryHandler : QueriesHandlerBaseClass, IRequestHandler<GetWordOfTheDayQuery, WordCount>
        {
            public GetWordOfTheDayQueryHandler(IWordsMongoDb context) : base(context)
            {
            }
            public async Task<WordCount> Handle(GetWordOfTheDayQuery query, CancellationToken cancellationToken)
            {
                var wordOfTheDay = _context.WordCounts
                .AsQueryable()
                .OrderByDescending(w => w.Count)
                .FirstOrDefault();

                if (wordOfTheDay == null)
                    return null;

                return  wordOfTheDay;
            }
        }
    }
}
