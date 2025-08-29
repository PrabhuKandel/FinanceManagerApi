using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Domain.Models;
using FinanceManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace FinanceManager.Infrastructure.Repositories
{
    public class TransactionCategoryRepository : ITransactionCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
            public async Task<IEnumerable<TransactionCategory>> GetAllAsync()
            {
                return await _context.TransactionCategories.ToListAsync();
            }
        public async Task<TransactionCategory> GetByIdAsync(Guid id , bool isTracking = true)
        {
            IQueryable<TransactionCategory> query = _context.TransactionCategories;

            if (!isTracking)
                query = query.AsNoTracking();

            return await  query.FirstOrDefaultAsync(tc=>tc.Id==id);
        }

        public async  Task AddAsync(TransactionCategory transactionCategory)
        {
             await _context.TransactionCategories.AddAsync(transactionCategory);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TransactionCategory transactionCategory)
        {
            
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(TransactionCategory transactionCategory)
        {
            _context.TransactionCategories.Remove(transactionCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.TransactionCategories.AnyAsync(c => c.Name == name);
        }

       
 

  

    }
}
