using System.Data;
using Ardalis.GuardClauses;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.PatchTransactionRecord
{
    public class PatchTransactionRecordDapperHandler(IDbConnection connection,IApplicationDbContext context, IUserContext userContext) : IRequestHandler<PatchTransactionRecordDapperCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(PatchTransactionRecordDapperCommand request, CancellationToken cancellationToken)
        {
            var transactionRecordFromDb = await context.TransactionRecords
               .Include(tr => tr.TransactionPayments)
               .FirstOrDefaultAsync(tr => tr.Id == request.Id, cancellationToken);

            Guard.Against.Null(transactionRecordFromDb, nameof(transactionRecordFromDb), "Transaction record not found");

            //validating amount
            decimal totalAmount = request.Amount ?? transactionRecordFromDb.Amount;

            var updatedPayments = request.Payments != null && request.Payments.Any()
                ? request.Payments.Select(p => p.Amount).Sum()
                : transactionRecordFromDb.TransactionPayments.Sum(p => p.Amount);

            if (totalAmount != updatedPayments)
            {
                throw new BusinessValidationException("Total transaction amount must equal sum of payments");
            }


            // Prepare table-valued parameter for payments if provided
            DataTable? paymentsDataTable = null;
            if (request.Payments != null && request.Payments.Any())
            {
                paymentsDataTable = new DataTable();
                paymentsDataTable.Columns.Add("PaymentMethodId", typeof(Guid));
                paymentsDataTable.Columns.Add("Amount", typeof(decimal));

                foreach (var p in request.Payments)
                    paymentsDataTable.Rows.Add(p.PaymentMethodId, p.Amount);
            }

            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id);
            parameters.Add("TransactionCategoryId", request.TransactionCategoryId);
            parameters.Add("Amount", request.Amount);
            parameters.Add("TransactionDate", request.TransactionDate);
            parameters.Add("Description", request.Description);
            parameters.Add("UpdatedByApplicationUserId", userContext.UserId);
            if (paymentsDataTable != null)
                parameters.Add("Payments", paymentsDataTable.AsTableValuedParameter("TransactionPaymentType"));


            var rows = await connection.QueryAsync("usp_PatchTransactionRecord",
                parameters
                , commandType: CommandType.StoredProcedure);

            var result = TransactionRecordDapperMapper.MapTransactionRecordResults(rows);
            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "Transaction record patched",
                Data = result.FirstOrDefault()
            }; 
        }
    }
}
