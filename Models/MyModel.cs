using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bank.Models

{
    public class Transactions
    {
        [Key]
        public int TransId { get; set; }
        public Double Amount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User AccountHolder { get; set; }
        public int UserId { get; set; }

    }
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public List<Transactions> Trans { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Must be longer than 2")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
       
    }

    public class LoginUser
    {
        // Other fields
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}