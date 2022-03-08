using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.WordFeatures.Queries.QueryExtensions;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using MediatR;
using Application.Interfaces;
using Application.Features.WordFeatures.Queries.QueriesHandlersBaseClasses;
using MongoDB.Driver;
using System.Linq;

namespace Application.Features.WordFeatures.Queries
{
    public class GetClosestWordsQuery : IRequest<IEnumerable<WordCount>>
    {
        public string Text { get; set; }
        public GetClosestWordsQuery(string text)
        {
            Text = text;
        }
        public class GetClosestWordsQueryHandler : QueriesHandlerBaseClass, IRequestHandler<GetClosestWordsQuery, IEnumerable<WordCount>>
        {
            public GetClosestWordsQueryHandler(IWordsMongoDb context) : base(context)
            {
            }
            public async Task<IEnumerable<WordCount>> Handle(GetClosestWordsQuery query, CancellationToken cancellationToken)
            {
                var word = query.Text;

                var keys = GetKeys(word);

                var closeWords = _context.WordCounts
                    .AsQueryable()
                    .FilterClosestWords(keys, word);

                return closeWords.ToList();
            }
            internal static List<string> GetKeys(string word)
            {
                var keys = new List<string>(){ $"{word}" };
                var len = word.Length;

                if (word.Length == 1)
                {
                    return keys;
                }
                else if (word.Length == 2)
                {
                    keys.Add($"{word[0]}.{word[1]}");
                }

                keys = GenerateInternalKeys(keys, len, word);

                return keys;
            }
            internal static List<string> GenerateInternalKeys(List<string> keys, int len, string word)
            {
                for (var i = 0; i < len; i++)
                {
                    if (i == 0)
                    {
                        keys.Add($"{word.Substring(1, len - 1)}");
                    }
                    else if (i == len - 1)
                    {
                        keys.Add($"{word.Substring(0, i)}");
                    }
                    else
                    {
                        keys.Add($"{word.Substring(0, i)}.{word.Substring(i + 1, len - i - 1)}");
                        keys.Add($"{word.Substring(0, i)}{word.Substring(i + 1, len - i - 1)}");
                    }
                }
                keys = keys.Distinct().ToList();

                return keys;
            }
        }
    }
}
