using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankTestAPI.Models.Enum;

namespace BankTestAPI.Models
{
    public class Account : BaseEntity
    {
        [Required]
        public decimal Balance { get; private set; }

        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }

        internal bool Withdraw(Transaction transaction) 
        {
            if(TransactionType.Debit.Equals(transaction.Type))
            {
                Balance -= transaction.Value;
                Transactions.Add(transaction);
                return true;
            }

            return false;
        }

       internal bool Deposit(Transaction transaction) 
        {
            if(TransactionType.Credit.Equals(transaction.Type))
            {
                Balance += transaction.Value;
                Transactions.Add(transaction);
                return true;
            }

            return false;
        }
    }
}