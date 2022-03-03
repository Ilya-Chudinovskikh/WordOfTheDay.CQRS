using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;
using System.Linq;
using MongoDB.Driver;

namespace Application.Features.WordFeatures.Queries
{
    public class GetUserWordQuery : IRequest<WordCount>
    {
        public string Text { get; set; }
        public GetUserWordQuery(string text)
        {
            Text = text;
        }
        public class GetUserWordQueryHandler : QueriesHandlerBaseClass, IRequestHandler<GetUserWordQuery, WordCount>
        {
            public GetUserWordQueryHandler(IWordsMongoDb context) : base(context)
            {
            }
            public async Task<WordCount> Handle(GetUserWordQuery query, CancellationToken cancellationToken)
            {
                var userWord = await _context.WordCounts
                    .AsQueryable()
                    .SingleOrDefaultAsync(wordCount=>query.Text==wordCount.Word, cancellationToken); 

                return userWord;
            }
        }
    }
}
