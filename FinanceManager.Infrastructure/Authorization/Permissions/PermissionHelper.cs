using System.Reflection;


namespace FinanceManager.Infrastructure.Authorization.Permissions
{
    public class PermissionDto
    {
        public string Group { get; set; } = null!;
        public string Permission { get; set; } = null!;
    }

    public static class PermissionHelper
    {
        public static IEnumerable<PermissionDto> GetAllPermissions()
        {
            var permissions = new List<PermissionDto>();

            var nestedTypes = typeof(PermissionConstants).GetNestedTypes();
            foreach (var type in nestedTypes)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

                permissions.AddRange(
                fields.Select(f => f.GetValue(null)?.ToString())
                   .Where(v => !string.IsNullOrWhiteSpace(v))
                   .Select(v => new PermissionDto
                   {
                       Group = type.Name,
                       Permission = v!
                   })
         );


            }

            return permissions;
        }
     public static IEnumerable<string> GetAdminPermissions() => GetAllPermissions().Select(p => p.Permission);
        public static IEnumerable<string> GetUserPermissions() => new[]
        {
        PermissionConstants.TransactionRecordPermissions.View,
        PermissionConstants.TransactionRecordPermissions.Create,
        PermissionConstants.TransactionRecordPermissions.Update,
        PermissionConstants.TransactionRecordPermissions.Export,

        PermissionConstants.TransactionCategoryPermissions.View,
        PermissionConstants.TransactionCategoryPermissions.Create,
        PermissionConstants.TransactionCategoryPermissions.Update,

        PermissionConstants.PaymentMethodPermissions.View,
        PermissionConstants.PaymentMethodPermissions.Create,
        PermissionConstants.PaymentMethodPermissions.Update,

        };
    }


    }
