using FinanceManager.Application.Exceptions;
using System;
using FinanceManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinanceManager.Infrastructure.Data.Configurations;

namespace FinanceManager.Infrastructure.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<TransactionRecord> TransactionRecords { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
          

            modelBuilder.ApplyConfiguration(new TransactionRecordConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());


        }



    }
}
