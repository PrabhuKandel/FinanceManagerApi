

using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    public class TransactionPaymentConfiguration : IEntityTypeConfiguration<TransactionPayment>
    {
        public void Configure(EntityTypeBuilder<TransactionPayment> builder)
        {   
           builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.Amount)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");  // 18 digits total, 2 after the decimal

            // Relationship with TransactionRecord
            builder.HasOne(tp => tp.TransactionRecord)
                   .WithMany(tr => tr.TransactionPayments)
                   .HasForeignKey(tp => tp.TransactionRecordId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship with PaymentMethod
            builder.HasOne(tp => tp.PaymentMethod)
                .WithMany(pm => pm.TransactionPayments)
                .HasForeignKey(tp => tp.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
