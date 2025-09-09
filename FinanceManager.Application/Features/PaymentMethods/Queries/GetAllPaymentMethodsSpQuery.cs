using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Queries
{
    public record GetAllPaymentMethodsSpQuery() : IRequest<OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {

    }

    public class GetAllPaymentMethodsSpHandler(ApplicationDbContext context) : IRequestHandler<GetAllPaymentMethodsSpQuery, OperationResult<IEnumerable<PaymentMethodResponseDto>>>
{
        public async Task<OperationResult<IEnumerable<PaymentMethodResponseDto>>> Handle(GetAllPaymentMethodsSpQuery request, CancellationToken cancellationToken)
        {
          var paymentMethods = await context.PaymentMethods.FromSqlRaw("EXECUTE dbo.usp_GetAllPaymentMethods").ToListAsync(cancellationToken);
            var paymentMethodDtos = paymentMethods.ToResponseDtoList();
            return new OperationResult<IEnumerable<PaymentMethodResponseDto>>
            {


                Data = paymentMethodDtos,
                Message = "Payment methods retrieved successfully"


            };

        }
    }
}
