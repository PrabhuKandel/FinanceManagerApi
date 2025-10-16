using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using FinanceManager.Application.Features.TransactionCategories.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.CreateTransactionCategory
{
    public class CreateTransactionCategorySpHandler : IRequestHandler<CreateTransactionCategorySpCommand, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public CreateTransactionCategorySpHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(CreateTransactionCategorySpCommand request, CancellationToken cancellationToken)
        {
            var transactionCategory = context.TransactionCategories.
                FromSqlInterpolated($"EXEC dbo.usp_CreateTransactionCategory @Name = {request.Name}, @Description = {request.Description}, @Type = {request.Type}")
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();


            return new OperationResult<TransactionCategoryResponseDto>
            {

                Message = "New transaction category added",
                Data = transactionCategory?.ToResponseDto()
            };


        }
    }
}
