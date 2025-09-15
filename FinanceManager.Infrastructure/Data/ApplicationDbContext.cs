using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinanceManager.Infrastructure.Data.Configurations;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Dtos.TransactionRecord;


namespace FinanceManager.Infrastructure.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser, IdentityRole, string>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<TransactionRecord> TransactionRecords { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<TransactionRecordSpResult> TransactionRecordSpResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mark the SP result DTO as a "keyless entity"
            modelBuilder.Entity<TransactionRecordSpResult>().HasNoKey();


            modelBuilder.ApplyConfiguration(new TransactionRecordConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());


        }



    }
}
