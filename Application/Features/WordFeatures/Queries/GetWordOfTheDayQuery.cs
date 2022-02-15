using Application.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueryExtensions;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Application.Features.WordFeatures.Queries
{
    public class GetWordOfTheDayQuery : IRequest<WordCount>
    {
        public GetWordOfTheDayQuery()
        {
        }
        public class GetWordOfTheDayQueryHandler : IRequestHandler<GetWordOfTheDayQuery, WordCount>
        {
            private readonly IWordsDbContext _context;
            public GetWordOfTheDayQueryHandler(IWordsDbContext context)
            {
                _context = context;
            }
            private static DateTime DateToday
            {
                get { return DateTime.Today.ToUniversalTime(); }
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
