using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.Shared;
using FinanceManager.Application.Features.TransactionRecords.Commands.BulkCreate;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Queries.ImportFromExcel
{
    public class ImportTransactionRecordsHandler(IApplicationDbContext context, IMediator mediator) : IRequestHandler<ImportTransactionRecordsQuery, OperationResult<string>>
    {

        public async Task<OperationResult<string>> Handle(ImportTransactionRecordsQuery request, CancellationToken cancellationToken)
        {
            // Load category and payment method mappings from database
            var categoryMap = await context.TransactionCategories
                .ToDictionaryAsync(c => c.Name.Trim(), c => c.Id, cancellationToken);

            var paymentMap = await context.PaymentMethods
                .ToDictionaryAsync(p => p.Name.Trim(), p => p.Id, cancellationToken);


            using var stream = request.ExcelFile.OpenReadStream();
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);

            var rows = worksheet.RowsUsed().Skip(1); // skip header
            var totalRows = rows.Count();

            var transactionRows = new List<BulkCreateTransactionRecordDto>();
            var errors = new List<string>();

            foreach (var row in rows)
            {
                var transactionDate = GetDateTime(row.Cell(1));

                var category = GetString(row.Cell(2));
                if (!categoryMap.TryGetValue(category, out var transactionCategoryId))
                    throw new Exception($"Unknown Transaction Category '{category}' in row {row.RowNumber()}");

                var amount = GetDecimal(row.Cell(3));
                var description = GetString(row.Cell(4));

                var paymentMethodName = GetString(row.Cell(5));
                if (!paymentMap.TryGetValue(paymentMethodName, out var paymentMethodId))
                    throw new Exception($"Unknown Payment Method '{paymentMethodName}' in row {row.RowNumber()}");

                var paymentAmount = GetDecimal(row.Cell(6));


                var payment = new TransactionPaymentDto
                {
                    PaymentMethodId = paymentMethodId,
                    Amount = paymentAmount
                };

                transactionRows.Add(new BulkCreateTransactionRecordDto
                {
                    TransactionDate = transactionDate,
                    TransactionCategoryId = transactionCategoryId,
                    Amount = amount,
                    Description = description,
                    Payments = new List<TransactionPaymentDto> { payment }
                });

            }

            var bulkCommand = new BulkCreateTransactionRecordCommand(transactionRows);
            var result = await mediator.Send(bulkCommand, cancellationToken);

   
            // Call bulk command

            return new OperationResult<string>
            {
                Message = "Transaction records imported successfully."
            };
        }

        #region Helpers

        private static DateTime GetDateTime(IXLCell cell)
        {
            if (DateTime.TryParse(cell.GetString(), out var date))
                return date;
            else
                throw new Exception($"Invalid date in cell {cell.Address}: '{cell.GetString()}'");
        }

        private static decimal GetDecimal(IXLCell cell)
        {
            if (decimal.TryParse(cell.GetString(), out var value))
                return value;
            throw new Exception($"Invalid decimal in cell {cell.Address}: '{cell.GetString()}'");
        }

        private static string GetString(IXLCell cell)
        {
            return cell.GetString()?.Trim() ?? string.Empty;
        }

        #endregion
    }
}
