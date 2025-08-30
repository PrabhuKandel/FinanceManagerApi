using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Interfaces.Repositories;

namespace FinanceManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITransactionCategoryRepository TransactionCategory { get; private set; }
        public IPaymentMethodRepository PaymentMethod { get; private set; }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
