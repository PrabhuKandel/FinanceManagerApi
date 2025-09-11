using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.UpdateTransactionCategory
{
    public class UpdateTransactionCategorySpHandler : IRequestHandler<UpdateTransactionCategorySpCommand, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly ApplicationDbContext context;

        public UpdateTransactionCategorySpHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(UpdateTransactionCategorySpCommand request, CancellationToken cancellationToken)
        {
            var transactionCategory = context.TransactionCategories
            .FromSqlInterpolated($"EXEC dbo.usp_UpdateTransactionCategory @Id = {request.Id}, @Name = {request.Name}, @Description = {request.Description}, @Type = {request.Type}")
            .AsNoTracking()
            .AsEnumerable()
            .FirstOrDefault();


            return new OperationResult<TransactionCategoryResponseDto>
            {

                Message = "Transaction category udpated",
                Data = transactionCategory?.ToResponseDto()
            };


        }
    }
}
