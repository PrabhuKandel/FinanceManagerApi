using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ITransactionCategoryRepository TransactionCategory { get; }
        IPaymentMethodRepository PaymentMethod { get; }

        void Save();
    }
}
