using System.Data;
using Ardalis.GuardClauses;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.UpdateTransactionRecord
{
    public class UpdateTransactionRecordDapperHandler(IDbConnection connection, IUserContext userContext, IApplicationDbContext context) : IRequestHandler<UpdateTransactionRecordDapperCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(UpdateTransactionRecordDapperCommand request, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();

            // Load TransactionRecord with related payments
            var transactionRecord = await context.TransactionRecords
                .Include(tr => tr.TransactionPayments)
                .FirstOrDefaultAsync(tr => tr.Id == request.Id, cancellationToken);

            Guard.Against.Null(transactionRecord, nameof(transactionRecord), "Transaction record not found");

            //optimization remaining.....
            // Preload active payment method IDs
            var activePaymentMethodIds = await context.PaymentMethods
                .Where(pm => pm.IsActive)
                .Select(pm => pm.Id)
                .ToHashSetAsync(cancellationToken);

            // Loop through incoming payments
            for (int i = 0; i < request.Payments.Count; i++)
            {
                var payment = request.Payments[i];

                // Skip existing payments
                if (transactionRecord.TransactionPayments.Any(tp => tp.PaymentMethodId == payment.PaymentMethodId))
                    continue;

                // PaymentMethodId validation
                if (!activePaymentMethodIds.Contains(payment.PaymentMethodId))
                    errors[$"TransactionRecord.Payments[{i}].PaymentMethodId"] = new[] { "Invalid or inactive payment method" };

                // Amount validation
                if (payment.Amount <= 0)
                    errors[$"TransactionRecord.Payments[{i}].Amount"] = new[] { "Payment amount must be greater than 0" };
            }

            // Throw if any errors
            if (errors.Any())
                throw new BusinessValidationException(errors);



            //prepare table valued parameter 
            var paymentsDataTable = new DataTable();
            paymentsDataTable.Columns.Add("PaymentMethodId", typeof(Guid));
            paymentsDataTable.Columns.Add("Amount", typeof(decimal));

            foreach (var p in request.Payments)
                paymentsDataTable.Rows.Add(p.PaymentMethodId, p.Amount);

            var parameters = new DynamicParameters();
            parameters.Add("Id", request.Id); // transaction record ID
            parameters.Add("TransactionCategoryId", request.TransactionCategoryId);
            parameters.Add("Amount", request.Amount);
            parameters.Add("TransactionDate", request.TransactionDate);
            parameters.Add("Description", request.Description);
            parameters.Add("UpdatedByApplicationUserId", userContext.UserId);
            parameters.Add("Payments", paymentsDataTable.AsTableValuedParameter("TransactionPaymentType"));



            var rows = await connection.QueryAsync("usp_UpdateTransactionRecord",
                 parameters,
                 commandType: CommandType.StoredProcedure);

            var result = TransactionRecordMappingProfile.MapTransactionRecordResults(rows);

            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "Transaction record updated",
                Data = result.FirstOrDefault()
            };

        }

    }
}
