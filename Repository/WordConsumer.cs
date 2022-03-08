using SharedModelsLibrary;
using MassTransit;
using System;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;
using Application.Features.WordFeatures.Commands;
using Application.Features.WordFeatures.Queries;

namespace Repository
{
    public class WordConsumer : IConsumer<WordInfo>
    {
        private readonly IMediator _mediator;
        public WordConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<WordInfo> wordInfoIncoming)
        {
            var wordCount = new WordCount
            {
                Word = wordInfoIncoming.Message.Text,
                Count = 1,
                expireAt = wordInfoIncoming.Message.AddTime.AddDays(1)
            };
            var word = wordCount.Word;

            if (await _mediator.Send(new WordIsAlreadyExistsQuery(word)))
                await _mediator.Send(new IncrementCountCommand(word));

            else
                await _mediator.Send(new ConsumeCommand(wordCount));
        }
    }
}
