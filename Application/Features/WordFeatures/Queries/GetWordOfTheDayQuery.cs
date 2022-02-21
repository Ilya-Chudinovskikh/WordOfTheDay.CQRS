using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueryExtensions;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;
using MongoDB.Driver;
//using MongoDB.Driver.Linq;

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
                var wordOfTheDay = _context.Words
                .AsQueryable()
                .LaterThan(DateToday)
                .GroupByWordCount()
                .OrderByDescending(w => w.Count)
                .FirstOrDefault(/*cancellationToken: cancellationToken*/);

                if (wordOfTheDay == null)
                    return null;

                return  wordOfTheDay;
            }
        }
    }
}
