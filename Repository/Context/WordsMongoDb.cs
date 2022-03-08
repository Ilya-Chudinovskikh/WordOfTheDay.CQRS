using Application.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Domain.Models;
using System;

namespace Repository.Context
{
    public class WordsMongoDb : IWordsMongoDb
    {
        public WordsMongoDb(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("WordsMongoDb"));
            var database = client.GetDatabase("WordsMongoDb");

            WordCounts = database.GetCollection<WordCount>("WordCounts");
        }
        public IMongoCollection<WordCount> WordCounts { get; set; }
    }
}
