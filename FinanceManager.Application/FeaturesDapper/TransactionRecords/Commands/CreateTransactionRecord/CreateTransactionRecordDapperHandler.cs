using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Mapping;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Enums;
using MediatR;
using System.Data;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.CreateTransactionRecord
{
    public class CreateTransactionRecordDapperHandler(IDbConnection connection, IUserContext userContext) : IRequestHandler<CreateTransactionRecordDapperCommand, OperationResult<TransactionRecordResponseDto>>
        {
            public async Task<OperationResult<TransactionRecordResponseDto>> Handle(CreateTransactionRecordDapperCommand request, CancellationToken cancellationToken)
            {


            // Set ApprovalStatus based on user role
            var approvalStatus = userContext.IsAdmin()
                ? TransactionRecordApprovalStatus.Approved
                : TransactionRecordApprovalStatus.Pending;


            //prepare table valued parameter 
            var paymentsDataTable = new DataTable();
                paymentsDataTable.Columns.Add("PaymentMethodId", typeof(Guid));
                paymentsDataTable.Columns.Add("Amount", typeof(decimal));

                foreach (var p in request.Payments)
                    paymentsDataTable.Rows.Add(p.PaymentMethodId, p.Amount);

                var parameters = new DynamicParameters();
                parameters.Add("TransactionCategoryId", request.TransactionCategoryId);
                parameters.Add("Amount", request.Amount);
                parameters.Add("TransactionDate", request.TransactionDate);
                parameters.Add("Description", request.Description);
                parameters.Add("CreatedByApplicationUserId", userContext.UserId);
                parameters.Add("ApprovalStatus", approvalStatus);
                parameters.Add("Payments", paymentsDataTable.AsTableValuedParameter("TransactionPaymentType"));



                var rows = await connection.QueryAsync
                    ("usp_CreateTransactionRecord",
                    parameters, 
                    commandType: CommandType.StoredProcedure);

          

                var result = TransactionRecordMappingProfile.MapTransactionRecordResults(rows);

                return new OperationResult<TransactionRecordResponseDto>
                {
                    Message = "New transaction record added",
                    Data =     result.FirstOrDefault()
            };

            }

    }
}
