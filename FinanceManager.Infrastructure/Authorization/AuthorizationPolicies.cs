

using Microsoft.AspNetCore.Authorization;

namespace FinanceManager.Infrastructure.Authorization
{
    public static class AuthorizationPolicies
    {
        public static void AddPermissionPolicies(AuthorizationOptions options)
        {
            var allPermissions = new[]
            {
                PermissionConstants.TransactionCategoryPermissions.View,
                PermissionConstants.TransactionCategoryPermissions.Create,
                PermissionConstants.TransactionCategoryPermissions.Update,
                PermissionConstants.TransactionCategoryPermissions.Delete
       
            };
                    
            foreach (var permission in allPermissions)
            {
                options.AddPolicy(permission, policy =>
                    policy.RequireClaim(permission, "true"));
            }   
        }
    }
}
