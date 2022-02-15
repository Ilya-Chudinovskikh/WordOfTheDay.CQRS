using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueryExtensions;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using MediatR;
using Application.Interfaces;
using LinqKit;

namespace Application.Features.WordFeatures.Queries
{
    public class GetClosestWordsQuery : IRequest<IEnumerable<WordCount>>
    {
        public string Email { get; set; }
        public GetClosestWordsQuery(string email)
        {
            Email = email;
        }
        public class GetClosestWordsQueryHandler : IRequestHandler<GetClosestWordsQuery, IEnumerable<WordCount>>
        {
            private readonly IWordsDbContext _context;
            public GetClosestWordsQueryHandler(IWordsDbContext context)
            {
                _context = context;
            }
            private static DateTime DateToday
            {
                get { return DateTime.Today.ToUniversalTime(); }
            }
            public async Task<IEnumerable<WordCount>> Handle(GetClosestWordsQuery query, CancellationToken cancellationToken)
            {
                var word = (await UserWord(query.Email)).Word;

                var keys = GetKeys(word);

                var closeWords = _context.Words
                    .AsExpandable().FilterClosestWords(keys, word)
                    .LaterThan(DateToday)
                    .GroupByWordCount();

                return closeWords.ToList();
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
            public static List<string> GetKeys(string word)
            {
                var keys = new List<string>();
                var len = word.Length;

                for (var i = 0; i < len; i++)
                {
                    if (i == 0)
                    {
                        keys.Add($"%{word.Substring(1, len - 1)}");
                    }
                    else if (i == len - 1)
                    {
                        keys.Add($"{word.Substring(0, i)}%");
                    }
                    else
                    {
                        keys.Add($"{word.Substring(0, i)}%{word.Substring(i + 1, len - i - 1)}");
                    }
                }

                return keys;
            }
        }
    }
}
