using FinanceManager.Application.Common;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.DeletePaymentMethod
{

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
