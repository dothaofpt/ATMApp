using Microsoft.EntityFrameworkCore;
using ATMApp.Models;

namespace ATMApp.Data{
    public class ATMContext : DbContext{
        public ATMContext(DbContextOptions<ATMContext> options):base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<User>()
           .HasMany(u =>u.Accounts)
           .WithOne(a =>a.User)
           .HasForeignKey(a =>a.UserId);

           modelBuilder.Entity<Account>()
           .HasMany(a => a.Transactions)
           .WithOne(t => t.Account)
           .HasForeignKey(t => t.AccountId);

        }
    }

}