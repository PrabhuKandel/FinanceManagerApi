using FinanceManager.Application.Common;
using FinanceManager.Application.Features.PaymentMethods.Dtos;
using FinanceManager.Application.Features.PaymentMethods.Mapping;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.UpdatePaymentMethod
{
    public class UpdatePaymentMethodSpHandler(IApplicationDbContext context) : IRequestHandler<UpdatePaymentMethodSpCommand, OperationResult<PaymentMethodResponseDto>>
    {

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(UpdatePaymentMethodSpCommand request, CancellationToken cancellationToken)
        {

            var paymentMethod = context.PaymentMethods
            .FromSqlInterpolated(
                $"EXEC dbo.usp_UpdatePaymentMethod @Id = {request.Id}, @Name = {request.Name}, @IsActive = {request.IsActive}, @Description = {request.Description}"
            )
            .AsNoTracking()
            .AsEnumerable()
            .FirstOrDefault();


            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "Payment method updated",
                Data = paymentMethod?.ToResponseDto()
            };


        }
    }
}
