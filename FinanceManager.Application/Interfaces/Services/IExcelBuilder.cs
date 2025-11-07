
using ClosedXML.Excel;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface IExcelBuilder<TData>
    {
        XLWorkbook Build(TData data);
    }
}
