using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    class ConfigureWordsMongoDbIndexesService
    {
        private readonly IWordsMongoDb _wordsMongoDb;
        public ConfigureWordsMongoDbIndexesService(IConfiguration configuration)
        {
            _wordsMongoDb = new WordsMongoDb(configuration);
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var indexKeysDefinition = Builders<WordCount>.IndexKeys.Ascending(x => x.expireAt);
            var indexOptions = new CreateIndexOptions { ExpireAfter = new TimeSpan(0, 0, 0) };
            await _wordsMongoDb.WordCounts.Indexes.CreateOneAsync(new CreateIndexModel<WordCount>(indexKeysDefinition), cancellationToken: cancellationToken);
        }


        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
