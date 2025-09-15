using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FinanceManager.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TransactionCategory> TransactionCategories { get; set; }
        DbSet<PaymentMethod> PaymentMethods { get; set; }
        DbSet<TransactionRecord> TransactionRecords { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
         DbSet<TransactionRecordSpResult> TransactionRecordSpResults { get; set; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
