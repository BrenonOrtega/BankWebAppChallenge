using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BankTestAPI.Models.Enum;

namespace BankTestAPI.Models
{
    public partial class Transaction : BaseEntity
    {
        [Required, EnumDataType(typeof(TransactionType)), JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType Type { get; set; }

        public int AccountId;
        
        [Required, ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public decimal Value { get; set; }
    }
}
