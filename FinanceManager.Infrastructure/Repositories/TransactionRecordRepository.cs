using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Domain.Models;
using FinanceManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Repositories
{
    public class TransactionRecordRepository : ITransactionRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TransactionRecord>> GetAllAsync(String userId)
        {
            return await BaseQuery(userId).ToListAsync();
        }
        public async Task<TransactionRecord?> GetByIdAsync(Guid id, String userId)
        {
            return await BaseQuery(userId).FirstOrDefaultAsync(tr => tr.Id == id);

        }
        public async Task<TransactionRecord?> AddAsync(TransactionRecord transactionRecord)
        {
            await _context.TransactionRecords.AddAsync(transactionRecord);
            await _context.SaveChangesAsync();

            return await BaseQuery(transactionRecord.ApplicationUserId)
           .FirstOrDefaultAsync(tr => tr.Id == transactionRecord.Id);
           

        }
        public async Task UpdateAsync(TransactionRecord transactionRecord)
        {

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TransactionRecord transactionRecord)
        {
            _context.TransactionRecords.Remove(transactionRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionRecord>> FilterTransactionRecordsAsync(
            String userId,  decimal? minAmount, decimal? maxAmount, Guid? transactionCategory, Guid? paymentMethod, DateTime? transactionDate)
        {
            var query = BaseQuery(userId);

            if (minAmount.HasValue && minAmount.Value>0)
                query = query.Where(t => t.Amount >= minAmount.Value);

            if (maxAmount.HasValue && maxAmount.Value > 0)
                query = query.Where(t => t.Amount <= maxAmount.Value);

            if (transactionCategory.HasValue)
                query = query.Where(t => t.TransactionCategoryId == transactionCategory.Value);

            if (paymentMethod.HasValue)
                query = query.Where(t => t.PaymentMethodId == paymentMethod.Value);

            if(transactionDate.HasValue && transactionDate.Value != DateTime.MinValue)
                query = query.Where(t => t.TransactionDate.Date == transactionDate.Value.Date);

            return await query.ToListAsync(); 
        }


        // Private helper method that includes common query
        private IQueryable<TransactionRecord> BaseQuery(string userId)
        {
            return _context.TransactionRecords
                .Where(tr => tr.ApplicationUserId == userId)
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.PaymentMethod);
        }
    }
}
