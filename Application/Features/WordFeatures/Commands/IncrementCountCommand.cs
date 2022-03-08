using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Commands.CommandsExtensions;

namespace Application.Features.WordFeatures.Commands
{
    public class IncrementCountCommand : IRequest<string>
    {
        public string Word;
        public IncrementCountCommand(string word)
        {
            Word = word;
        }
        public class IncrementCountCommandHandler : IRequestHandler<IncrementCountCommand, string>
        {
            private readonly IWordsMongoDb _context;
            public IncrementCountCommandHandler(IWordsMongoDb context)
            {
                _context = context;
            }
            public async Task<string> Handle(IncrementCountCommand command, CancellationToken cancellationToken)
            {
                var word = command.Word;

                await _context.WordCounts.IncrementCount(word);

                return word;
            }
        }
    }
}
