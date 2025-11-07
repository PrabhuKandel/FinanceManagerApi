

using ClosedXML.Excel;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface IExcelService
    {
        byte[] SaveWorkbook(XLWorkbook workbook);
    }
}
