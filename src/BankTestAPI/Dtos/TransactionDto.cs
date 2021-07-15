using System;
using System.ComponentModel.DataAnnotations;
using BankTestAPI.Models.Enum;

namespace BankTestAPI.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required, EnumDataType(typeof(TransactionType))]
        public TransactionType TransactionType { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public int AccountId { get; set; }

        public int? RelatedAccountId { get; set; }

    }
}