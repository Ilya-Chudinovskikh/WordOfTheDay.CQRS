using Application.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueryExtensions;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;

namespace Application.Features.WordFeatures.Queries
{
    public class GetWordOfTheDayQuery : IRequest<WordCount>
    {
        public GetWordOfTheDayQuery()
        {
        }
        public class GetWordOfTheDayQueryHandler : QueriesHandlerBaseClass, IRequestHandler<GetWordOfTheDayQuery, WordCount>
        {
            public GetWordOfTheDayQueryHandler(IWordsDbContext context) : base(context)
            {
            }
            public async Task<WordCount> Handle(GetWordOfTheDayQuery query, CancellationToken cancellationToken)
            {
                var wordOfTheDay = await _context.Words
                .LaterThan(DateToday)
                .GroupByWordCount()
                .OrderByDescending(w => w.Count)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (wordOfTheDay == null)
                    return null;

                return wordOfTheDay;
            }
        }
    }
}
