using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Domain.Entities;
using MediatR;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.CreateTransactionCategory
{
    public  record  CreateTransactionCategorySpCommand (string Name, string? Description, CategoryType Type) : IRequest<OperationResult<TransactionCategoryResponseDto>>
    {
    }
}
