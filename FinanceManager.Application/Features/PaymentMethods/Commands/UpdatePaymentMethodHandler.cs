using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.CreatePaymentMethod
{
    public  class UpdatePaymentMethodHandler(ApplicationDbContext _context) : IRequestHandler<UpdatePaymentMethodCommand, OperationResult<PaymentMethodResponseDto>>
    {

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {

            var paymentMethod = await _context.PaymentMethods.FindAsync(request.Id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method doesn't exist");
            }

            paymentMethod.UpdateEntity(request.paymentMethod);
            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "Payment method updated",
                Data = paymentMethod.ToResponseDto()
            };


        }
    }

}