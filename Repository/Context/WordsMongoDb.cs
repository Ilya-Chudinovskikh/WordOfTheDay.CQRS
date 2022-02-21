using Application.Interfaces;
using Domain.Entites;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Repository.Context
{
    public class WordsMongoDb : IWordsMongoDb
    {
        public WordsMongoDb(IConfiguration configuration)
        {
            //var client = new MongoClient(configuration.GetValue<string>("QueriesMongoDbSettings:ConnectionString"));
            //var database = client.GetDatabase(configuration.GetValue<string>("QueriesMongoDbSettings:DatabaseName"));

            //Words = database.GetCollection<Word>(configuration.GetValue<string>("QueriesMongoDbSettings:CollectionName"));
            var client = new MongoClient(configuration.GetConnectionString("WordsMongoDb"));
            var database = client.GetDatabase("WordsMongoDb");

            Words = database.GetCollection<Word>("Words");
        }
        public IMongoCollection<Word> Words { get; set; }
        public WordsMongoDb()
        {
        }
    }
}
