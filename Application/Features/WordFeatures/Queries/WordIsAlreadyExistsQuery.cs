using Application.Features.WordFeatures.Queries.QueryExtensions;
using Application.Interfaces;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;

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
            public WordIsAlreadyExistQueryHandler(IWordsDbContext context) : base(context)
            {
            }
            public Task<bool> Handle(WordIsAlreadyExistsQuery query, CancellationToken cancellationToken)
            {
                var exist = _context.Words
                .LaterThan(DateToday)
                .ByEmail(query.Word.Email)
                .AnyAsync(cancellationToken: cancellationToken);

                return exist;
            }
        }
    }
}
