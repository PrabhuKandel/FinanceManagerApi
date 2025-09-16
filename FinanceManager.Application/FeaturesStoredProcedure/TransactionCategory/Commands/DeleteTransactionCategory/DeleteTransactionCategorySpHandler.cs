using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.DeleteTransactionCategory
{
    public class DeleteTransactionCategorySpHandler(IApplicationDbContext context) : IRequestHandler<DeleteTransactionCategorySpCommand, OperationResult<string>>
    {

        public async Task<OperationResult<string>> Handle(DeleteTransactionCategorySpCommand request, CancellationToken cancellationToken)
        {

            await context.Database.ExecuteSqlInterpolatedAsync(
              $"EXEC dbo.usp_DeleteTransactionCategory @Id = {request.Id}");

            return new OperationResult<string>
            {

                Message = "Transaction category deleted",

            };


         


        }
    }
}
