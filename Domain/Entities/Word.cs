using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entites
{
    public class Word
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        [BsonElement("Email")]
        [Required(ErrorMessage = "Enter your email!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your word!")]
        [StringLength(50, ErrorMessage = "Your word must be no more than 50 characters long!")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Your word must be in English, without using spaces and special characters!")]
        [BsonElement("Text")]
        public string Text { get; set; }
        [Required]
        public DateTime AddTime { get; set; }
        public double LocationLongitude { get; set; }
        public double LocationLatitude { get; set; }
    }
}
