

using ClosedXML.Excel;
using FinanceManager.Application.Interfaces.Services;

namespace FinanceManager.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public byte[] SaveWorkbook(XLWorkbook workbook)
        {
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
