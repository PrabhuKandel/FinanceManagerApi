using FinanceManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        DatabaseFacade Database { get; }    
        DbSet<RefreshToken> RefreshTokens { get; set; }

        DbSet<TransactionPayment> TransactionPayments { get; set; }

        DbSet<IdentityUserRole<string>> UserRoles { get; set; }

        DbSet<IdentityRole> Roles { get; set; }



        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
