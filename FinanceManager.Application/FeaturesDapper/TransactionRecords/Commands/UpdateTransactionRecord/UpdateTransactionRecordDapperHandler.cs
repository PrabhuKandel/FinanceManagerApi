using System.Data;
using System.Data.Common;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.UpdateTransactionRecord
{
    public class UpdateTransactionRecordDapperHandler(IDbConnection connection, IUserContext userContext) : IRequestHandler<UpdateTransactionRecordDapperCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(UpdateTransactionRecordDapperCommand request, CancellationToken cancellationToken)
        {


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

            var result = TransactionRecordDapperMapper.MapTransactionRecordResults(rows);

            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "Transaction record updated",
                Data = result.FirstOrDefault()
            };

        }

    }
}
