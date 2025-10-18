using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    public class PermissionConfiguration :IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
    {
           

            // Primary key
            builder.HasKey(p => p.Id);

          
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(p => p.Name)
                .IsUnique();  // Ensure Name is unique

        
            builder.Property(p => p.Description)
                .HasMaxLength(512);

      
            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);
        }
  
}
}