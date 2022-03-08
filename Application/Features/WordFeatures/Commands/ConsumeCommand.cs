using Domain.Models;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.WordFeatures.Commands
{
    public class ConsumeCommand : IRequest<WordCount>
    {
        public WordCount _wordCount;
        public ConsumeCommand(WordCount wordCount)
        {
            _wordCount = wordCount;
        }
        public class ConsumeCommandHandler : IRequestHandler<ConsumeCommand, WordCount>
        {
            private readonly IWordsMongoDb _context;
            public ConsumeCommandHandler(IWordsMongoDb context)
            {
                _context = context;
            }
            public async Task<WordCount> Handle(ConsumeCommand command, CancellationToken cancellationToken)
            {
                await Console.Out.WriteLineAsync(command._wordCount.ToString());

                await _context.WordCounts.InsertOneAsync(command._wordCount, cancellationToken);

                return command._wordCount;
            }
        }
    }
}
