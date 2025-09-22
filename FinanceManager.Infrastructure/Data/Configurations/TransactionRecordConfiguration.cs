using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static System.Formats.Asn1.AsnWriter;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    public class TransactionRecordConfiguration : IEntityTypeConfiguration<TransactionRecord>
    {
        public void Configure(EntityTypeBuilder<TransactionRecord> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TransactionCategoryId)
                   .IsRequired();



            //"decimal(18,2)" means:18 → total number of digits(precision) 2 → number of digits after the decimal point(scale)
            builder.Property(e => e.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

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


            builder.HasOne(e => e.TransactionCategory)
                   .WithMany()
                   .HasForeignKey(e => e.TransactionCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);


             builder.HasOne(tr => tr.CreatedByApplicationUser)             // Each TransactionRecord has one User
                     .WithMany(u => u.CreatedTransactionsRecords)          // Each User has many TransactionRecords
                    .HasForeignKey(tr => tr.CreatedByApplicationUserId)
                     .IsRequired() 
                    .OnDelete(DeleteBehavior.Restrict);         

            builder.HasOne(tr => tr.UpdatedByApplicationUser)            
               .WithMany(u => u.UpdatedTransactionsRecords)         
              .HasForeignKey(tr => tr.UpdatedByApplicationUserId)
              .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
