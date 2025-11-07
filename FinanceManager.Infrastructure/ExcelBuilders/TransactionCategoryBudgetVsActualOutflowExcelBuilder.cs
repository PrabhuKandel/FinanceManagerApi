

using ClosedXML.Excel;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Enums;

namespace FinanceManager.Infrastructure.ExcelBuilders
{
    public class TransactionCategoryBudgetVsActualOutflowExcelBuilder : IExcelBuilder<IEnumerable<TransactionCategoryBudgetVsActualOutflowDto>>
    {
        public XLWorkbook Build(IEnumerable<TransactionCategoryBudgetVsActualOutflowDto> data)
        {
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Budget vs Actual");

            // === Top metadata ===
            var periodStart = data.FirstOrDefault()?.PeriodStart ?? DateTime.MinValue;
            var periodEnd = data.FirstOrDefault()?.PeriodEnd ?? DateTime.MinValue;
            var periodType = data.FirstOrDefault()?.PeriodType.ToString() ?? string.Empty;


            ws.Cell("A1").Value = "Period Type:";
            ws.Cell("B1").Value = periodType.ToString();

            ws.Cell("A2").Value = "Period Start:";
            ws.Cell("B2").Value = periodStart.ToString("dd-MMM-yyyy");

            ws.Cell("A3").Value = "Period End:";
            ws.Cell("B3").Value = periodEnd.ToString("dd-MMM-yyyy");

            ws.Range("A1:A3").Style.Font.Bold = true;


            // === Table headers ===
            int startRow = 5;
            ws.Cell(startRow, 1).Value = "Transaction Category";
            ws.Cell(startRow, 2).Value = "Budget Amount (Rs)";
            ws.Cell(startRow, 3).Value = "Actual Amount (Rs)";
            ws.Cell(startRow, 4).Value = "Remaining Budget (Rs)";
            ws.Cell(startRow, 5).Value = "Budget Usage (%)";

            ws.Range(startRow, 1, startRow, 8).Style.Font.Bold = true;
            ws.Range(startRow, 1, startRow, 8).Style.Fill.BackgroundColor = XLColor.LightGray;

            // === Data rows ===
            int row = startRow + 1;
            foreach (var item in data)
            {
                ws.Cell(row, 1).Value = item.TransactionCategoryName;

                var budgetCell = ws.Cell(row, 2);
                budgetCell.Value = item.BudgetAmount;
                budgetCell.Style.NumberFormat.Format = "#,##0.00";

                // Actual Amount
                var actualCell = ws.Cell(row, 3);
                actualCell.Value = item.ActualAmount;
                actualCell.Style.NumberFormat.Format = "#,##0.00";

                // Remaining Budget
                var remainingCell = ws.Cell(row, 4);
                remainingCell.Value = item.RemainingBudget;
                remainingCell.Style.NumberFormat.Format = "#,##0.00";
                remainingCell.Style.Font.FontColor = item.RemainingBudget < 0 ? XLColor.Red : XLColor.Green;


                // Budget Usage %
                var usageCell = ws.Cell(row, 8);
                usageCell.Value = item.BudgetUsagePercent / 100;
                usageCell.Style.NumberFormat.Format = "0.00%";
                usageCell.Style.Font.FontColor = item.BudgetUsagePercent > 100 ? XLColor.Red : XLColor.Green;


                row++;
            }

            ws.Columns().AdjustToContents();
            return wb;
        }
    }
}
