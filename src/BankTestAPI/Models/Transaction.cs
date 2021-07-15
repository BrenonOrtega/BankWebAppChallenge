using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankTestAPI.Models.Enum;

namespace BankTestAPI.Models
{
    public partial class Transaction : BaseEntity
    {
        [Required, EnumDataType(typeof(TransactionType))]
        public TransactionType Type { get; set; }

        public int AccountId { get; set;}
        
        [Required, ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        public int RelatedAccountId { get; set;}
        [ForeignKey(nameof(RelatedAccountId))]
        public Account RelatedAccount { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Value { get; set; }
    }
}
