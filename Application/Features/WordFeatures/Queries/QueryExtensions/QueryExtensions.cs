using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Entites;
using Domain.Models;
using MongoDB.Driver.Linq;

namespace Application.Features.WordFeatures.Queries.QueryExtensions
{
    internal static class QueryExtensions
    {
        public static IQueryable<Word> LaterThan(this IQueryable<Word> query, DateTime today)
        {
            query = query.Where(word => word.AddTime >= today);

            return query;
        }
        public static List<WordCount> FilterClosestWords(this IQueryable<WordCount> query, List<string> keys, string word)
        {
            var closeWords = new List<WordCount>();

            foreach (string key in keys)
            {
                AddWordsByKey(query, closeWords, key, word);
            }

            return closeWords;
        }
        private static List<WordCount> AddWordsByKey(IQueryable<WordCount> query, List<WordCount> closeWords,string key, string word )
        {
            var regex = new Regex(key);
            var words = query.Where(wordCount => regex.IsMatch(wordCount.Word)
            && wordCount.Word.Length <= key.Replace(".","").Length + 1
            && wordCount.Word != word);

            closeWords.AddRange(words);

            return closeWords;
        }
    }
}
