

using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Application.Dtos.Shared
{
    public class SummaryDtos
    {
        public class TransactionCategorySummaryDto
        {
            public Guid Id { get; set; }
            public required string Name { get; set; }
        }

        public class TransactionPaymentSummaryDto
        {
            public required Guid PaymentMethodId { get; set; }
            public required string Name { get; set; }
            public required decimal Amount { get; set; }
        }

        public class ApplicationUserSummaryDto
        {
            public required string Id { get; set; }
            public required string Email { get; set; }
        }
    }
}
