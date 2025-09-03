using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace FinanceManager.Infrastructure.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentMethodRepository(ApplicationDbContext context)
        {
            _context = context;
        }
            public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
            {
                return await _context.PaymentMethods.ToListAsync();
            }
        public async Task<PaymentMethod> GetByIdAsync(Guid id , bool isTracking = true)
        {
            IQueryable<PaymentMethod> query = _context.PaymentMethods;

            if (!isTracking)
                query = query.AsNoTracking();

            return await  query.FirstOrDefaultAsync(tc=>tc.Id==id);
        }

        public async  Task AddAsync(PaymentMethod paymentMethod)
        {
             await _context.PaymentMethods.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PaymentMethod paymentMethod)
        {
            
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.PaymentMethods.AnyAsync(c => c.Name == name);
        }
        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _context.PaymentMethods.AnyAsync(c => c.Id == id);
        }






    }
}
