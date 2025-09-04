using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethod.Commands
{
    public class DeletePaymentMethodHandler(ApplicationDbContext _context) : IRequestHandler<DeletePaymentMethodCommand, string>
    {
        public async Task<string> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(request.Id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method doesn't exist");
            }
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync(cancellationToken);
            return "Payment Method deleted successfully";
        }
    }
}
