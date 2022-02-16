using Application.Features.WordFeatures.Queries.QueryExtensions;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses
{
    public class QueriesWithUserWordHandlerBaseClass : QueriesHandlerBaseClass
    {
        public QueriesWithUserWordHandlerBaseClass(IWordsDbContext context) : base(context)
        {
        }
        public async Task<WordCount> UserWord(string email)
        {
            var word = await _context.Words
                .LaterThan(DateToday)
                .ByEmail(email)
                .SingleOrDefaultAsync();

            var userWordAmount = await _context.Words
                .LaterThan(DateToday)
                .ByText(word.Text)
                .CountAsync();

            var userWord = new WordCount { Word = word.Text, Count = userWordAmount };

            return userWord;
        }
    }
}
