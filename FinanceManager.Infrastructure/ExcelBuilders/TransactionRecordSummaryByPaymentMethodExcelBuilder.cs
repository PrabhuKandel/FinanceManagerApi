using ClosedXML.Excel;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.Interfaces.Services;

namespace FinanceManager.Infrastructure.ExcelBuilders
{
    public class TransactionRecordSummaryByPaymentMethodExcelBuilder : IExcelBuilder<IEnumerable<TransactionRecordSummaryByPaymentMethodDto>>
    {
        public XLWorkbook Build(IEnumerable<TransactionRecordSummaryByPaymentMethodDto> data)
        {
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("SummaryByPaymentMethod ");

            // === Header row ===
            int startRow = 1;
            ws.Cell(startRow, 1).Value = "Payment Method";
            ws.Cell(startRow, 2).Value = "Total Amount (Rs)";
            ws.Cell(startRow, 3).Value = "Total Transactions";

            ws.Range(startRow, 1, startRow, 3).Style.Font.Bold = true;
            ws.Range(startRow, 1, startRow, 3).Style.Fill.BackgroundColor = XLColor.LightGray;

            // === Data rows ===
            int row = startRow + 1;
            foreach (var item in data)
            {
                // Payment Method
                ws.Cell(row, 1).Value = item.PaymentMethodName;

                // Total Amount
                var amountCell = ws.Cell(row, 2);
                amountCell.Value = item.TotalAmount;
                amountCell.Style.NumberFormat.Format = "#,##0.00";

                // Total Transactions
                var transactionCell = ws.Cell(row, 3);
                transactionCell.Value = item.TotalTransactions;
                transactionCell.Style.NumberFormat.Format = "#,##0";

                row++;
            }

            // Adjust column widths
            ws.Columns().AdjustToContents();

            return wb;
        }
    }
}
