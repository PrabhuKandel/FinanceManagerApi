using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;

using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public class DeletePaymentMethodHandler : IRequestHandler<DeletePaymentMethodCommand, OperationResult<string>>
    {
        private readonly IApplicationDbContext context;

        public DeletePaymentMethodHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<string>> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = await context.PaymentMethods.FindAsync(request.Id);
            context.PaymentMethods.Remove(paymentMethod);
            await context.SaveChangesAsync(cancellationToken);
            return new OperationResult<string>
            {

               Message = "Payment method deleted",

            };
        }
    }
}
