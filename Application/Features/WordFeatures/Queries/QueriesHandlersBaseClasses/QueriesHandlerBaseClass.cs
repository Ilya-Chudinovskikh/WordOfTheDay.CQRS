using Application.Interfaces;

namespace Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses
{
    public class QueriesHandlerBaseClass
    {
        private protected readonly IWordsMongoDb _context;
        public QueriesHandlerBaseClass(IWordsMongoDb context)
        {
            _context = context;
        }
    }
}
