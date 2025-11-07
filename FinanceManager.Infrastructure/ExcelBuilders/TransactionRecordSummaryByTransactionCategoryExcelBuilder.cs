using ClosedXML.Excel;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.Interfaces.Services;

namespace FinanceManager.Infrastructure.ExcelBuilders
{
    public class TransactionRecordSummaryByTransactionCategoryExcelBuilder : IExcelBuilder<IEnumerable<TransactionRecordSummaryByCategoryDto>>
    {
        public XLWorkbook Build(IEnumerable<TransactionRecordSummaryByCategoryDto> data)
        {
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("SummaryByTransactionCategory");

            // === Header row ===
            int startRow = 1;
            ws.Cell(startRow, 1).Value = "Transaction Category";
            ws.Cell(startRow, 2).Value = "Category Type";
            ws.Cell(startRow, 3).Value = "Total Amount (Rs)";
            ws.Cell(startRow, 4).Value = "Total Transactions";

            ws.Range(startRow, 1, startRow, 4).Style.Font.Bold = true;
            ws.Range(startRow, 1, startRow, 4).Style.Fill.BackgroundColor = XLColor.LightGray;

            // === Data rows ===
            int row = startRow + 1;
            foreach (var item in data)
            {
                // Payment Method
                ws.Cell(row, 1).Value = item.TransactionCategoryName;

                // Category Type
                ws.Cell(row, 2).Value = item.CategoryType;
                // Total Amount
                var amountCell = ws.Cell(row, 3);
                amountCell.Value = item.TotalAmount;
                amountCell.Style.NumberFormat.Format = "#,##0.00";

                // Total Transactions
                var transactionCell = ws.Cell(row, 4);
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
