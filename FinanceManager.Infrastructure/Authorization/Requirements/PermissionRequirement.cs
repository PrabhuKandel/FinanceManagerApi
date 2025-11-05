

using Microsoft.AspNetCore.Authorization;

namespace FinanceManager.Infrastructure.Authorization.Requirements
{
    //defines what needs to be checked
    public class PermissionRequirement:IAuthorizationRequirement
    {
        public string PermissionName { get; }

        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
