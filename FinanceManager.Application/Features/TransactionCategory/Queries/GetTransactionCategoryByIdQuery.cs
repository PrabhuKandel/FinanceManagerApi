using FinanceManager.Application.Dtos.TransactionCategory;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategory.Queries
{

    // previously used class instead of record and throws error check it later......
    public record GetTransactionCategoryByIdQuery(Guid Id):IRequest<TransactionCategoryResponseDto>
    {
    }
}
