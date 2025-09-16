using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;


namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.CreatePaymentMethod
{
    public class CreatePaymentMethodDapperHandler(IDbConnection connection) : IRequestHandler<CreatePaymentMethodDapperCommand, OperationResult<PaymentMethodResponseDto>>
    {

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(CreatePaymentMethodDapperCommand request, CancellationToken cancellationToken)
        {


            var paymentMethod = await connection.QuerySingleOrDefaultAsync<PaymentMethodResponseDto>("usp_CreatePaymentMethod", new { request.Name, request.Description, request.IsActive }, commandType: CommandType.StoredProcedure);

            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "New payment method added",

                Data = paymentMethod
            };


        }

    }
}
