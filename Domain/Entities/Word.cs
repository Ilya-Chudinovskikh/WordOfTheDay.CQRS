﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entites
{
    public class Word
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Enter your email!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your word!")]
        [StringLength(50, ErrorMessage = "Your word must be no more than 50 characters long!")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Your word must be in English, without using spaces and special characters!")]
        public string Text { get; set; }
        [Required]
        public DateTime AddTime { get; set; }
        public double LocationLongitude { get; set; }
        public double LocationLatitude { get; set; }
    }
}
