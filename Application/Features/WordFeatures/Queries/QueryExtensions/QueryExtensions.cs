using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entites;
using Domain.Models;
using LinqKit;
using MongoDB.Driver;
using Application.Interfaces;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Misc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace Application.Features.WordFeatures.Queries.QueryExtensions
{
    internal static class QueryExtensions
    {
        //public static async Task<IAsyncCursor<Word>> LaterThan(this IMongoCollection<Word> collection, DateTime today)
        //{
        //    var resultCollection = await collection.FindAsync(word => word.AddTime >= today);

        //    return resultCollection;
        //}
        //public static async Task<IAsyncCursor<Word>> ByEmail(this IMongoCollection<Word> collection, string email)
        //{
        //    var resultCollection = await collection.FindAsync(word => word.Email == email);

        //    return resultCollection;
        //}
        //public static async Task<IAsyncCursor<Word>> ByText(this IMongoCollection<Word> collection, string text)
        //{
        //    var resultCollection = await collection.FindAsync(word => word.Text == text);

        //    return resultCollection;
        //}
        //public static async Task<IAsyncCursor<Word>> GroupByWordCount(this IMongoCollection<Word> collection)
        //{
        //    var resultCollection = await collection.Aggregate()
        //        .Where(word => word.Text)
        //        .Group((text, words) => new WordCount { Word = text, Count = words.Count(word => word.Text == text) }
        //        );

        //    return resultCollection;
        //}
        public static IQueryable<Word> LaterThan(this IQueryable<Word> query, DateTime today)
        {
            query = query.Where(word => word.AddTime >= today);

            return query;
        }
        public static IQueryable<Word> ByEmail(this IQueryable<Word> query, string email)
        {
            query = query.Where(word => word.Email == email);

            return query;
        }
        public static IQueryable<Word> ByText(this IQueryable<Word> query, string text)
        {
            query = query.Where(word => word.Text == text);

            return query;
        }
        public static IQueryable<WordCount> GroupByWordCount(this IQueryable<Word> query)
        {
            var result = query.GroupBy(word => word.Text, (text, words) => new WordCount { Word = text, Count = words.Count(word => word.Text == text) });

            return result;
        }
        public static IQueryable<Word> FilterClosestWords(this IQueryable<Word> query, List<string> keys, string word)
        {
            var predicate = PredicateBuilder.New<Word>();

            foreach (string key in keys)
            {
                predicate = predicate.Or(
                    closeWord => EF.Functions.Like(closeWord.Text, key)
                    && closeWord.Text.Length <= word.Length + 1
                    && closeWord.Text != word);
            }

            query = query.Where(predicate);

            return query;
        }
    }
}
