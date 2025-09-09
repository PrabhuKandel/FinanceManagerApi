using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public  record  DeletePaymentMethodSpCommand(Guid Id): IRequest<OperationResult<String>>
    {
    }
    public class DeletePaymentMethodSpHandler(ApplicationDbContext context) : IRequestHandler<DeletePaymentMethodSpCommand, OperationResult<string>>
    {

        public async Task<OperationResult<string>> Handle(DeletePaymentMethodSpCommand request, CancellationToken cancellationToken)
        {
            await context.Database.ExecuteSqlInterpolatedAsync(
              $"EXEC dbo.usp_DeletePaymentMethod @Id = {request.Id}");

            return new OperationResult<string>
            {

                Message = "Payment method deleted",

            };
        }
    }

}
