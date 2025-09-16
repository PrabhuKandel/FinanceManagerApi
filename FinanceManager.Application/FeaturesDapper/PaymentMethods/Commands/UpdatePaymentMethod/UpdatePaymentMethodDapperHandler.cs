using System.Data;
using Ardalis.GuardClauses;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.UpdatePaymentMethod
{
    public class UpdatePaymentMethodDapperHandler(IDbConnection connection ) : IRequestHandler<UpdatePaymentMethodDapperCommand, OperationResult<PaymentMethodResponseDto>>
    {

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(UpdatePaymentMethodDapperCommand request, CancellationToken cancellationToken)
        {


            var paymentMethod = await connection.QuerySingleOrDefaultAsync<PaymentMethodResponseDto>("usp_UpdatePaymentMethod", new {  request.Id,request.Name, request.Description, request.IsActive }, commandType: CommandType.StoredProcedure);
            Guard.Against.Null(paymentMethod, nameof(paymentMethod), "Payment method not found");

            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "Payment method updated",
                Data = paymentMethod
            };


        }
    }
}
