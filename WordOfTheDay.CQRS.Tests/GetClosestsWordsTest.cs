using System;
using Xunit;
using Application.Features.WordFeatures.Queries;
using System.Collections.Generic;
using Domain.Models;
using System.Text.RegularExpressions;
using System.Linq;

namespace WordOfTheDay.CQRS.Tests
{
    public class GetClosestsWordsTest
    {
        private static List<WordCount> WordCounts
        {
            get
            {
                int date = 1;
                return new List<WordCount>
                {
                    new WordCount {Word = "abc", Count = 1, expireAt = new DateTime(2022, 3, date).ToUniversalTime()},
                    new WordCount {Word = "ab", Count = 2, expireAt = new DateTime(2022, 3, date).ToUniversalTime()},
                    new WordCount {Word = "bc", Count = 1, expireAt = new DateTime(2022, 3, date).ToUniversalTime()},
                    new WordCount {Word = "ac", Count = 3, expireAt = new DateTime(2022, 3, date).ToUniversalTime()}
                };
            }
        }
        [Fact]
        public void GetKeysTest()
        {
            var word = "ac";

            var listKeys = GetClosestWordsQuery.GetClosestWordsQueryHandler.GetKeys(word);

            Assert.Equal(4, listKeys.Count);
            Assert.IsType<string>(listKeys[0]);
            Assert.Equal("ac", listKeys[0]);
            Assert.Equal("a.c", listKeys[1]);
            Assert.Equal("c", listKeys[2]);
            Assert.Equal("a", listKeys[3]);
        }
        [Fact]
        public void GetKeysTest1()
        {
            var word = "fgh";

            var listKeys = GetClosestWordsQuery.GetClosestWordsQueryHandler.GetKeys(word);

            Assert.Equal(5, listKeys.Count);
            Assert.IsType<string>(listKeys[0]);
            Assert.Equal("fgh", listKeys[0]);
            Assert.Equal("gh", listKeys[1]);
            Assert.Equal("f.h", listKeys[2]);
            Assert.Equal("fh", listKeys[3]);
            Assert.Equal("fg", listKeys[4]);
        }
        [Fact]
        public void GetKeysTest2()
        {
            var word = "a";

            var listKeys = GetClosestWordsQuery.GetClosestWordsQueryHandler.GetKeys(word);

            Assert.Single(listKeys);
            Assert.IsType<string>(listKeys[0]);
            Assert.Equal("a", listKeys[0]);
        }
        [Fact]
        public void GetKeysTest3()
        {
            var words = new List<string>(){ "abc","ac"};
            var regex = new Regex($"a.c");
            var filteredWords = words.Where(word => regex.IsMatch(word)).ToList();

            Assert.Single(filteredWords);
            Assert.Equal("abc", filteredWords[0]);
            Assert.Matches("a.c", "abc");
            Assert.Matches("f.h", "fgh");
        }
        [Fact]
        public void GetKeysTest4()
        {
            var word = "abcd";

            var listKeys = GetClosestWordsQuery.GetClosestWordsQueryHandler.GetKeys(word);

            Assert.Equal(7, listKeys.Count);
            Assert.IsType<string>(listKeys[0]);
            Assert.Equal("abcd", listKeys[0]);
            Assert.Equal("bcd", listKeys[1]);
            Assert.Equal("a.cd", listKeys[2]);
            Assert.Equal("acd", listKeys[3]);
            Assert.Equal("ab.d", listKeys[4]);
            Assert.Equal("abd", listKeys[5]);
            Assert.Equal("abc", listKeys[6]);
        }
        [Fact]
        public void GetKeysTest5()
        {
            var word = "abcde";

            var listKeys = GetClosestWordsQuery.GetClosestWordsQueryHandler.GetKeys(word);

            Assert.Equal(9, listKeys.Count);
            Assert.IsType<string>(listKeys[0]);
            Assert.Equal("abcde", listKeys[0]);
            Assert.Equal("bcde", listKeys[1]);
            Assert.Equal("a.cde", listKeys[2]);
            Assert.Equal("acde", listKeys[3]);
            Assert.Equal("ab.de", listKeys[4]);
            Assert.Equal("abde", listKeys[5]);
            Assert.Equal("abc.e", listKeys[6]);
            Assert.Equal("abce", listKeys[7]);
            Assert.Equal("abcd", listKeys[8]);
        }
    }
}
