using FinanceManager.Application.Common;
using FinanceManager.Application.Features.PaymentMethods.Dtos;
using FinanceManager.Application.Features.PaymentMethods.Mapping;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.CreatePaymentMethod
{
    public class CreatePaymentMethodSpHandler(IApplicationDbContext context) : IRequestHandler<CreatePaymentMethodSpCommand, OperationResult<PaymentMethodResponseDto>>
    {

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(CreatePaymentMethodSpCommand request, CancellationToken cancellationToken)
        {


            var paymentMethod = context.PaymentMethods.FromSqlInterpolated(
            $"EXEC dbo.usp_CreatePaymentMethod @Name = {request.Name}, @IsActive = {request.IsActive}, @Description = {request.Description}"
               ).AsNoTracking().AsEnumerable().FirstOrDefault();

            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "New payment method added",

                Data = paymentMethod?.ToResponseDto()
            };


        }
    }
}
