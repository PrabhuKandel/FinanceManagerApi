using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Domain.Entities;
using MediatR;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.UpdateTransactionCategory
{
    public record UpdateTransactionCategorySpCommand( Guid Id , string Name, string Description, CategoryType Type) : IRequest<OperationResult<TransactionCategoryResponseDto>>
    {
    }
}
