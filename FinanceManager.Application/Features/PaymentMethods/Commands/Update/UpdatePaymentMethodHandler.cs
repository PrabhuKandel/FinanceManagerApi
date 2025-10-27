using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.PaymentMethods.Dtos;
using FinanceManager.Application.Features.PaymentMethods.Mapping;
using FinanceManager.Application.Interfaces;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.Update
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
           Guard.Against.Null(paymentMethod, nameof(paymentMethod), "Payment method not found");
            paymentMethod.UpdateEntity(request);
            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "Payment method updated",
                Data = paymentMethod.ToResponseDto()
            };


        }
    }

}