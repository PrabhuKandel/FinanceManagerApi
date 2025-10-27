

namespace FinanceManager.Application.Features.PaymentMethods.Dtos
{
    public class PaymentMethodResponseDto
    {
        public Guid Id { get; set; }
        public required string  Name { get; set; }
        public string? Description { get; set; }

        public bool IsActive{ get; set; }
    }
}
