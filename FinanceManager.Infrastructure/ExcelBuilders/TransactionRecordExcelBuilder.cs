

using ClosedXML.Excel;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Interfaces.Services;

namespace FinanceManager.Infrastructure.ExcelBuilders
{
    public class TransactionRecordExcelBuilder : IExcelBuilder<IEnumerable<TransactionRecordExportDto>>
    {
        public XLWorkbook Build(IEnumerable<TransactionRecordExportDto> records)
        {
             var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Transaction Records");

            var headers = new[]
            {
                "SN", "Transaction Date", "Category", "Amount (Rs)", "Description",
                "Approval Status", "Created By", "Updated By", "Actioned By",
                "Payment Method", "Payment Amount (Rs)"
            };

            for (int i = 0; i < headers.Length; i++)
                ws.Cell(1, i + 1).Value = headers[i];

            var headerRange = ws.Range(1, 1, 1, headers.Length);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            int row = 2, sn = 1;
            foreach (var record in records)
            {
                var payments = record.Payments.Any()
                    ? record.Payments
                    : new List<PaymentExportDto> { new() { PaymentName = "-", Amount = 0m } };

                int startRow = row;
                foreach (var payment in payments)
                {
                    ws.Cell(row, 1).Value = sn;
                    ws.Cell(row, 2).Value = record.TransactionDate.ToString("yyyy-MM-dd");
                    ws.Cell(row, 3).Value = record.Category ?? "-";
                    ws.Cell(row, 4).Value = record.Amount;
                    ws.Cell(row, 5).Value = record.Description ?? "";
                    ws.Cell(row, 6).Value = record.ApprovalStatus?.ToString() ?? "-";
                    ws.Cell(row, 7).Value = record.CreatedBy ?? "-";
                    ws.Cell(row, 8).Value = record.UpdatedBy ?? "-";
                    ws.Cell(row, 9).Value = record.ActionedBy ?? "-";
                    ws.Cell(row, 10).Value = payment.PaymentName ?? "-";
                    ws.Cell(row, 11).Value = payment.Amount;
                    row++;
                }

                int endRow = row - 1;
                if (endRow > startRow)
                {
                    for (int col = 1; col <= 9; col++)
                    {
                        var merged = ws.Range(startRow, col, endRow, col);
                        merged.Merge();
                        merged.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                ws.Range(endRow, 1, endRow, headers.Length).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                sn++;
            }

            ws.Column(4).Style.NumberFormat.Format = "\"Rs\" #,##0.00";
            ws.Column(11).Style.NumberFormat.Format = "\"Rs\" #,##0.00";

            ws.Columns().AdjustToContents();
            ws.SheetView.FreezeRows(1);

            return workbook;
        }
    }
}
