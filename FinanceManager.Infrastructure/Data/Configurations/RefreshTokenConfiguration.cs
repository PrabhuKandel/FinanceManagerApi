using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration :IEntityTypeConfiguration<RefreshToken>
    {
     
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {

            // Primary key
            builder.HasKey(rt => rt.Id);

            // Properties
            builder.Property(rt => rt.Token)
                .IsRequired();
          

            builder.Property(rt => rt.CreatedAt)
                .IsRequired();

            builder.Property(rt => rt.ExpiresAt)
                .IsRequired();

            builder.Property(rt => rt.RevokedAt)
                .IsRequired(false);

            builder.Property(rt => rt.DeviceInfo)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(rt => rt.RevocationReason)
                .HasMaxLength(256)
                .IsRequired(false);

            // Relationship with User
            builder.HasOne(rt => rt.ApplicationUser)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(rt => rt.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade); // delete tokens if user is deleted

        }
    }
}
