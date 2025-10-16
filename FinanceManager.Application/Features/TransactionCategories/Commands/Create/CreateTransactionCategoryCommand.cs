using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using FinanceManager.Domain.Entities;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands.Create
{
    public  record CreateTransactionCategoryCommand
        (
        string Name,
        string? Description,
        CategoryType Type
      
        ):IRequest<OperationResult<TransactionCategoryResponseDto>>
    {

    }
}
