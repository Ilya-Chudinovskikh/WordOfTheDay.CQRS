using SharedModelsLibrary;
using MassTransit;
using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entites;

namespace Repository
{
    public class WordConsumer : IConsumer<WordInfo>
    {
        private readonly IWordsMongoDb _context;
        public WordConsumer(IWordsMongoDb context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<WordInfo> wordInfoIncoming)
        {
            await Console.Out.WriteLineAsync(wordInfoIncoming.Message.Text);

            _context.Words.InsertOne(
                new Word {
                    Id = wordInfoIncoming.Message.Id,
                    Email = wordInfoIncoming.Message.Email,
                    Text = wordInfoIncoming.Message.Text,
                    AddTime = wordInfoIncoming.Message.AddTime,
                    LocationLongitude = wordInfoIncoming.Message.LocationLongitude,
                    LocationLatitude = wordInfoIncoming.Message.LocationLatitude
                });
        }
    }
}