using System.Data;
using Ardalis.GuardClauses;
using Dapper;
using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.DeletePaymentMethod
{
    public class DeletePaymentMethodDapperHandler(IDbConnection connection) : IRequestHandler<DeletePaymentMethodDapperCommand, OperationResult<string>>
    {

        public async Task<OperationResult<string>> Handle(DeletePaymentMethodDapperCommand request, CancellationToken cancellationToken)
        {
            var result  =  await connection.ExecuteAsync("usp_DeletePaymentMethod", new { request.Id }, commandType: CommandType.StoredProcedure);
            //If no row matches (Id not found) → returns 0.
            Guard.Against.Zero(result, nameof(result),"Payment method not found");

            return new OperationResult<string>
            {

                Message = "Payment method deleted",

            };
        }
    }
}
