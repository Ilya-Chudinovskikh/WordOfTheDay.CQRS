using Application.Features.WordFeatures.Queries.QueryExtensions;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses
{
    public class QueriesWithUserWordHandlerBaseClass : QueriesHandlerBaseClass
    {
        public QueriesWithUserWordHandlerBaseClass(IWordsMongoDb context) : base(context)
        {
        }
        public async Task<WordCount> UserWord(string email)
        {
            var word = _context.Words
                .AsQueryable()
                .LaterThan(DateToday)
                .ByEmail(email)
                .SingleOrDefault();

            var userWordAmount = _context.Words
                .AsQueryable()
                .LaterThan(DateToday)
                .ByText(word.Text)
                .Count();

            var userWord = new WordCount { Word = word.Text, Count = userWordAmount };

            return userWord;
        }
    }
}
