
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Infrastructure.Authorization.Permissions;

namespace FinanceManager.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        public IEnumerable<string> GetAllPermissions()
        {
            return PermissionHelper.GetAllPermissions()
                             .Select(p => p.Permission);
        }
    }
}
