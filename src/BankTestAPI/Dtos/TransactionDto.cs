using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BankTestAPI.Models.Enum;

namespace BankTestAPI.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NonSerialized]
        public TransactionType transactionType;

        [Required(ErrorMessage = "Transaction type must be of type 'debit' or 'credit'")]
        public string TransactionType 
        { 
            get => transactionType.ToString(); 
            set {
                    Enum.TryParse(typeof(TransactionType), value, true, out var type); 
                    transactionType = (TransactionType) type;
                }
        }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public int AccountId { get; set; }
    }
}