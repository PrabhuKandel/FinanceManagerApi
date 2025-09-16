using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;
using System.Data;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.CreateTransactionRecord
{
    public class CreateTransactionRecordDapperHandler(IDbConnection connection, IUserContext userContext) : IRequestHandler<CreateTransactionRecordDapperCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(CreateTransactionRecordDapperCommand request, CancellationToken cancellationToken)
        {

            var transactionRecord = await connection.QuerySingleOrDefaultAsync<TransactionRecordDapperResult>("usp_CreateTransactionRecord",
                new {
                    request.TransactionCategoryId,
                    request.PaymentMethodId, 
                    request.Amount, 
                    request.Description,
                    request.TransactionDate,
                    CreatedByApplicationUserId = userContext.UserId,
                    UpdatedByApplicationUserId = userContext.UserId

                }
                , commandType: CommandType.StoredProcedure);

            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "New transaction record added",
                Data = transactionRecord?.ToResponseDtoFromDapper()
            };

        }

    }
}
