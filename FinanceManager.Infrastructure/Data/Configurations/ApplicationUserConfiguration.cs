using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            

          
            builder.Property(u => u.FirstName)
                   .IsRequired();

          
            builder.Property(u => u.LastName)
                   .IsRequired();

            builder.Property(u => u.Address)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.RefreshToken)
                   .IsRequired(false);

            builder.Property(u => u.RefreshTokenExpiresAtUtc)
                   .IsRequired(false);

        }
        }
}
