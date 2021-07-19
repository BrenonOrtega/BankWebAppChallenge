using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankTestAPI.Models
{
    public class User : BaseEntity
    {
        [Required, MaxLength(40)]
        public string FirstName { get; set; }

        [Required, MaxLength(60)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        public Account Account { get; set; }
        

        internal void Update(User updatedUser)
        {
            Account = updatedUser.Account;
            Email = updatedUser.Email;
            LastName = updatedUser.LastName;
            FirstName = updatedUser.LastName;
        }
    }
}