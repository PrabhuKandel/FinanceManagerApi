using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public class DeletePaymentMethodHandler : IRequestHandler<DeletePaymentMethodCommand, OperationResult<string>>
    {
        private readonly ApplicationDbContext context;

        public DeletePaymentMethodHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<string>> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = await context.PaymentMethods.FindAsync(request.Id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method doesn't exist");
            }
            context.PaymentMethods.Remove(paymentMethod);
            await context.SaveChangesAsync(cancellationToken);
            return new OperationResult<string>
            {

               Message = "Payment method deleted",

            };
        }
    }
}
