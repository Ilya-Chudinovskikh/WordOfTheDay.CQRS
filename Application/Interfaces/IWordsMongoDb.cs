using Domain.Models;
using MongoDB.Driver;

namespace Application.Interfaces
{
    public interface IWordsMongoDb
    {
        IMongoCollection<WordCount> WordCounts { get; set; }
    }
}
