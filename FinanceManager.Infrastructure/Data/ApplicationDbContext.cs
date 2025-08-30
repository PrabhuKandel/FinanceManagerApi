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

        public DbSet<TransactionRecord> TransactionRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<TransactionCategory>()
                  .HasIndex(c => c.Name)
                  .IsUnique();

         

        }



    }
}
