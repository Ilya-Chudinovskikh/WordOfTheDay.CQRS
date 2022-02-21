using Application.Features.WordFeatures.Queries.QueryExtensions;
using Application.Interfaces;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;
using MongoDB.Driver;

namespace Application.Features.WordFeatures.Queries
{
    public class WordIsAlreadyExistsQuery : IRequest<bool>
    {
        public Word Word { get; set; }
        public WordIsAlreadyExistsQuery(Word word)
        {
            Word = word;
        }
        public class WordIsAlreadyExistQueryHandler : QueriesHandlerBaseClass, IRequestHandler<WordIsAlreadyExistsQuery, bool>
        {
            public WordIsAlreadyExistQueryHandler(IWordsMongoDb context) : base(context)
            {
            }
            public async Task<bool> Handle(WordIsAlreadyExistsQuery query, CancellationToken cancellationToken)
            {
                var exist = _context.Words
                .AsQueryable()
                .LaterThan(DateToday)
                .ByEmail(query.Word.Email)
                .Any(/*cancellationToken: cancellationToken*/);

                return exist;
            }
        }
    }
}
