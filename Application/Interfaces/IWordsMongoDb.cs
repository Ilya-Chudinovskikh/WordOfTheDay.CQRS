using Domain.Entites;
using MongoDB.Driver;

namespace Application.Interfaces
{
    public interface IWordsMongoDb
    {
        IMongoCollection<Word> Words { get; set; }
    }
}
