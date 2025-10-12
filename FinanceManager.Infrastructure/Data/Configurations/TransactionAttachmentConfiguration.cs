using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Data.Configurations
{
    public class TransactionAttachmentConfiguration : IEntityTypeConfiguration<TransactionAttachment>
    {
    
        public void Configure(EntityTypeBuilder<TransactionAttachment> builder)
        {
          
            builder.HasKey(ta => ta.Id);

            builder.Property(ta => ta.Id)
                   .IsRequired();

          
            builder.Property(ta => ta.TransactionRecordId)
                   .IsRequired();

            builder.HasOne(ta => ta.TransactionRecord)
                   .WithMany(tr => tr.TransactionAttachments) 
                   .HasForeignKey(ta => ta.TransactionRecordId)
                   .OnDelete(DeleteBehavior.Cascade);

       
            builder.Property(ta => ta.UploadedByApplicationUserId)
                   .IsRequired();

            builder.HasOne(ta => ta.UploadedByApplicationUser)
                   .WithMany() 
                   .HasForeignKey(ta => ta.UploadedByApplicationUserId)
                   .OnDelete(DeleteBehavior.Restrict);

      
            builder.Property(ta => ta.FileName)
                   .IsRequired()
                   .HasMaxLength(255);

      
            builder.Property(ta => ta.FilePath)
                   .IsRequired()
                   .HasMaxLength(500);

    
            builder.Property(ta => ta.FileType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(ta => ta.UploadDate)
                   .IsRequired();
        }
    }
}
