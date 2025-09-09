using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public record  UpdatePaymentMethodSpCommand (Guid Id, PaymentMethodUpdateDto PaymentMethod): IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }
    public class UpdatePaymentMethodSpHandler(ApplicationDbContext context) : IRequestHandler<UpdatePaymentMethodSpCommand, OperationResult<PaymentMethodResponseDto>>
    {

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(UpdatePaymentMethodSpCommand request, CancellationToken cancellationToken)
        {

            var paymentMethod =  context.PaymentMethods
            .FromSqlInterpolated(
                $"EXEC dbo.usp_UpdatePaymentMethod @Id = {request.Id}, @Name = {request.PaymentMethod.Name}, @IsActive = {request.PaymentMethod.IsActive}, @Description = {request.PaymentMethod.Description}"
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
