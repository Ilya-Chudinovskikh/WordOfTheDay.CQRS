using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class WordCount
    {
        [Required]
        public string Word { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public DateTime expireAt { get; set; }
    }
}
