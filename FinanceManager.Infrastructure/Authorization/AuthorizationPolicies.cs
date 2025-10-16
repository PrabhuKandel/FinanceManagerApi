

using Microsoft.AspNetCore.Authorization;

namespace FinanceManager.Infrastructure.Authorization
{
    public static class AuthorizationPolicies
    {
        public static void AddPermissionPolicies(AuthorizationOptions options)
        {
            var allPermissions = new[]
            {
                PermissionConstants.TransactionCategoryPermissions.ViewOwn,
                PermissionConstants.TransactionCategoryPermissions.ViewAll,
                PermissionConstants.TransactionCategoryPermissions.Create,
                PermissionConstants.TransactionCategoryPermissions.UpdateOwn,
                PermissionConstants.TransactionCategoryPermissions.UpdateAll,
                PermissionConstants.TransactionCategoryPermissions.DeleteOwn,
                PermissionConstants.TransactionCategoryPermissions.DeleteAll,
            };
                    
            foreach (var permission in allPermissions)
            {
                options.AddPolicy(permission, policy =>
                    policy.RequireClaim(permission, "true"));
            }   
        }
    }
}
