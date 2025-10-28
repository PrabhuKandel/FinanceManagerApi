using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    public class BudgetConfiguration: IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            // Primary Key
            builder.HasKey(b => b.Id);

            builder.Property(b => b.TransactionCategoryId)
                   .IsRequired();
    
            builder.HasOne(b => b.TransactionCategory)
                   .WithMany()
                   .HasForeignKey(b => b.TransactionCategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.PeriodStart)
                   .IsRequired();

            builder.Property(b => b.PeriodEnd)
                   .IsRequired();

            builder.Property(b => b.Amount)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(b => b.Status)
                   .IsRequired();
            
            builder.Property(b => b.IsActive)
                     .IsRequired();
            // Audit fields
            builder.Property(b => b.CreatedByApplicationUserId)
                   .IsRequired();

            builder.Property(b => b.CreatedAt)
                   .IsRequired();



        }
    }
}
