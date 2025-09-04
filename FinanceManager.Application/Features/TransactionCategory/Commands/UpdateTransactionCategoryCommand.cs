using FinanceManager.Application.Dtos.TransactionCategory;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategory.Commands
{
    public  record UpdateTransactionCategoryCommand(Guid Id, TransactionCategoryUpdateDto transactionCategory):IRequest<TransactionCategoryResponseDto>
    {

    }
}
