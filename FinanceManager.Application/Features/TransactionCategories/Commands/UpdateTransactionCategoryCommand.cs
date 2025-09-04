using FinanceManager.Application.Dtos.TransactionCategory;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public  record UpdateTransactionCategoryCommand(Guid Id, TransactionCategoryUpdateDto transactionCategory):IRequest<TransactionCategoryResponseDto>
    {

    }
}
