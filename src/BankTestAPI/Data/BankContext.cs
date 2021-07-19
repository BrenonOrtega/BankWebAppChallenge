using BankTestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTestAPI.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options)
        {

        }

        

        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

    }
}