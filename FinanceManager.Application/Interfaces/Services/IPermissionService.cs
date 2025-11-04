

namespace FinanceManager.Application.Interfaces.Services
{
    public interface IPermissionService
    {
        IEnumerable<string> GetAllPermissions();
    }
}
