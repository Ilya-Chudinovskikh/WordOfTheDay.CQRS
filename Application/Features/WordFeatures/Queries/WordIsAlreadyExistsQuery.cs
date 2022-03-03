using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;
using MongoDB.Driver;

namespace Application.Features.WordFeatures.Queries
{
    public class WordIsAlreadyExistsQuery : IRequest<bool>
    {
        public string Text { get; set; }
        public WordIsAlreadyExistsQuery(string text)
        {
            Text = text;
        }
        public class WordIsAlreadyExistQueryHandler : QueriesHandlerBaseClass, IRequestHandler<WordIsAlreadyExistsQuery, bool>
        {
            public WordIsAlreadyExistQueryHandler(IWordsMongoDb context) : base(context)
            {
            }
            public async Task<bool> Handle(WordIsAlreadyExistsQuery query, CancellationToken cancellationToken)
            {
                var exist = _context.WordCounts
                .AsQueryable()
                .Any(word=>query.Text==word.Word);

                return exist;
            }
        }
    }
}
