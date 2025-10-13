using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using FinanceManager.Application.Features.TransactionCategories.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.UpdateTransactionCategory
{
    public class UpdateTransactionCategorySpHandler : IRequestHandler<UpdateTransactionCategorySpCommand, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public UpdateTransactionCategorySpHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(UpdateTransactionCategorySpCommand request, CancellationToken cancellationToken)
        {
            var transactionCategory = context.TransactionCategories
            .FromSqlInterpolated($"EXEC dbo.usp_UpdateTransactionCategory @Id = {request.Id}, @Name = {request.Name}, @Description = {request.Description}, @Type = {request.Type}")
            .AsNoTracking()
            .AsEnumerable()
            .FirstOrDefault();

            return new OperationResult<TransactionCategoryResponseDto>
            {

                Message = "Transaction category udpated",
                Data = transactionCategory?.ToResponseDto()
            };


        }
    }
}
