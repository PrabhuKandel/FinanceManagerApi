using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.CreatePaymentMethod
{
    public  class UpdatePaymentMethodHandler : IRequestHandler<UpdatePaymentMethodCommand, OperationResult<PaymentMethodResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public UpdatePaymentMethodHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {

            var paymentMethod = await context.PaymentMethods.FindAsync(request.Id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method doesn't exist");
            }

            paymentMethod.UpdateEntity(request.PaymentMethod);
            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "Payment method updated",
                Data = paymentMethod.ToResponseDto()
            };


        }
    }

}