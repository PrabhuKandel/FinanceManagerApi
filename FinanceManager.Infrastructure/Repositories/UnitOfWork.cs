using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Infrastructure.Data;

namespace FinanceManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ITransactionCategoryRepository TransactionCategory { get; private set; }
        public IPaymentMethodRepository PaymentMethod { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
