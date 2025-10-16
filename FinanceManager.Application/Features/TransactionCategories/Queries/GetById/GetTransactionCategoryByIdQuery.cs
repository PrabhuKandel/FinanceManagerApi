using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Queries.GetById
{


    public record GetTransactionCategoryByIdQuery(Guid Id):IRequest<OperationResult<TransactionCategoryResponseDto>>
    {
    }
}
