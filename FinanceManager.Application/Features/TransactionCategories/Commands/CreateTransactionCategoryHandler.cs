using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public  class CreateTransactionCategoryHandler : IRequestHandler<CreateTransactionCategoryCommand, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly ApplicationDbContext context;

        public CreateTransactionCategoryHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(CreateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {

            var entity = request.TransactionCategory.ToEntity();
            await context.TransactionCategories.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return new OperationResult<TransactionCategoryResponseDto>
            {

                Message = "New transaction category added",
                Data = entity.ToResponseDto()
            };


        }
    }

}