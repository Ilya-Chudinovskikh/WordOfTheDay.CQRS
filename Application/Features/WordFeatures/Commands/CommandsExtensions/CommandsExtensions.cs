using Domain.Models;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Application.Features.WordFeatures.Commands.CommandsExtensions
{
    public static class CommandsExtensions
    {
        public static async Task IncrementCount(this IMongoCollection<WordCount> query, string word)
        {
            var filter = Builders<WordCount>.Filter.Eq("Word", word);
            var update = Builders<WordCount>.Update.Inc(wordCount=>wordCount.Count, 1);

            await query.FindOneAndUpdateAsync(filter, update);
        }
    }
}
