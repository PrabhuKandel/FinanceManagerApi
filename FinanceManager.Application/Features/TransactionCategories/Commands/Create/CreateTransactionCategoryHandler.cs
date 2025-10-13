using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using FinanceManager.Application.Features.TransactionCategories.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;


namespace FinanceManager.Application.Features.TransactionCategories.Commands.Create
{
    public  class CreateTransactionCategoryHandler : IRequestHandler<CreateTransactionCategoryCommand, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public CreateTransactionCategoryHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(CreateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {

            var entity = request.ToEntity();
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