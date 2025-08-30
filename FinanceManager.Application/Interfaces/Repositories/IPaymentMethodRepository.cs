using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Repositories
{
    public interface IPaymentMethodRepository
    {
      
            Task<IEnumerable<PaymentMethod>> GetAllAsync();
            Task<PaymentMethod> GetByIdAsync(Guid id ,bool isTracking = true);
            Task  AddAsync(PaymentMethod paymentMethod);
            Task UpdateAsync( PaymentMethod paymentMethod);
            Task DeleteAsync(PaymentMethod paymentMethod);
             Task<bool> ExistsByNameAsync(string name);
             Task<bool> ExistByIdAsync(Guid id);
        //Task<IEnumerable<PaymentMethod>> GetByCategoryAsync(Guid categoryId); 
    }


}
