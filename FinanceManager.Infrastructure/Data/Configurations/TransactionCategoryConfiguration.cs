using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    public class TransactionCategoryConfiguration: IEntityTypeConfiguration<TransactionCategory>
    {
        public void Configure(EntityTypeBuilder<TransactionCategory> builder)
        {
           

            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.Id)
                   .IsRequired();

            builder.Property(tc => tc.Name)
                   .IsRequired()
                   .HasMaxLength(200);

               builder.HasIndex(c => c.Name)
                .IsUnique();


            builder.Property(tc => tc.Description)
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(tc => tc.Type)
                   .IsRequired();
        }
    }
}
