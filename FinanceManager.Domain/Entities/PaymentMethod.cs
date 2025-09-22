


namespace FinanceManager.Domain.Entities
{
    public class PaymentMethod
    {
       
        public Guid Id { get; set; } = Guid.NewGuid();

       
        public required string Name { get; set; }

      
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<TransactionPayment>? TransactionPayments { get; set; }
    }
}

