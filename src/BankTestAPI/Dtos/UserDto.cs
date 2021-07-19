using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BankTestAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        
        [Required, MaxLength(40)]
        public string FirstName { get; set; }

        [Required, MaxLength(60)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public int AccountId { get; set; }

    }
}