using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FinanceManager.Infrastructure.Data
{
    public class TransactionRecordConfiguration : IEntityTypeConfiguration<TransactionRecord>
    {
        public void Configure(EntityTypeBuilder<TransactionRecord> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TransactionCategoryId)
                   .IsRequired();

            builder.Property(e => e.PaymentMethodId)
                   .IsRequired();

            builder.Property(e => e.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.ToTable(t => t.HasCheckConstraint(
                name: "CK_TransactionRecord_Amount",
                sql: "Amount > 0.01"
            ));

            builder.Property(e => e.TransactionDate)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasMaxLength(500);

            builder.Property(e => e.CreatedAt)
                   .IsRequired();

            builder.Property(e => e.UpdatedAt)
                   .IsRequired();

            builder.Property(e => e.CreatedByApplicationUserId)
                   .IsRequired();


            builder.Property(e => e.UpdatedByApplicationUserId)
                   .IsRequired();

            builder.HasOne(e => e.TransactionCategory)
                   .WithMany()
                   .HasForeignKey(e => e.TransactionCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.PaymentMethod)
                   .WithMany()
                   .HasForeignKey(e => e.PaymentMethodId)   
                   .OnDelete(DeleteBehavior.Restrict);

             builder.HasOne(tr => tr.CreatedByApplicationUser)             // Each TransactionRecord has one User
                     .WithMany(u => u.CreatedTransactionsRecords)          // Each User has many TransactionRecords
                    .HasForeignKey(tr => tr.CreatedByApplicationUserId)
                     .IsRequired() 
                    .OnDelete(DeleteBehavior.Restrict);            // Cascade delete: deleting user deletes transactions

            builder.HasOne(tr => tr.UpdatedByApplicationUser)            
               .WithMany(u => u.UpdatedTransactionsRecords)         
              .HasForeignKey(tr => tr.UpdatedByApplicationUserId)
               .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
