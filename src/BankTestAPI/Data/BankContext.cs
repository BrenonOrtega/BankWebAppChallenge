using BankTestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTestAPI.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<User>().HasOne(user => user.Account).WithOne(account => account.Owner);
        //     modelBuilder.Entity<Account>().HasOne(account => account.Owner).WithOne(owner => owner.Account);

        //    //elBuilder.Entity<Account>().HasMany(account => account.Transactions).WithOne(Transaction => tr)
        // }   

        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

    }
}