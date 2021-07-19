using System;
using System.Collections.Generic;

namespace BankTestAPI.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }

        public decimal Balance { get; private set; }

        [NonSerialized]
        public int OwnerId;

        public UserDto Owner { get; set;}

        public List<TransactionDto> Transactions { get; set;}

        public AccountDto()
        {
            Transactions = new();
        }

        public bool IsEmpty()
        {
            return Id == 0 && Balance == 0 && OwnerId == 0;
        }
    }
}