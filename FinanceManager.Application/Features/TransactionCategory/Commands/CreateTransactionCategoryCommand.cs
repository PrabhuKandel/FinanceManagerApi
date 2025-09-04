using FinanceManager.Application.Dtos.TransactionCategory;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategory.Commands
{
    public  record CreateTransactionCategoryCommand(TransactionCategoryCreateDto transactionCategory):IRequest<TransactionCategoryResponseDto>
    {

    }
}
