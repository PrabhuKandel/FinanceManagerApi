using FinanceManager.Application.Exceptions;
using System;
using FinanceManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionCategory>().HasData(
                new TransactionCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Salary",
                    Description = "Monthly income from job",
                    Type = CategoryType.Income
                },
                new TransactionCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Freelance",
                    Description = "Freelance or side income",
                    Type = CategoryType.Income
                },
                new TransactionCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Food",
                    Description = "Groceries and dining out",
                    Type = CategoryType.Expense
                },
                new TransactionCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Transport",
                    Description = "Bus, train, fuel, ride shares",
                    Type = CategoryType.Expense
                },
                new TransactionCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Entertainment",
                    Description = "Movies, games, hobbies",
                    Type = CategoryType.Expense
                }
            );
                modelBuilder.Entity<TransactionCategory>()
                  .HasIndex(c => c.Name)
                  .IsUnique();

        }


    }
}
