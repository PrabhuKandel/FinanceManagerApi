    namespace FinanceManager.Infrastructure.Authorization.Permissions
{
    public static  class PermissionConstants
    {
        public static class TransactionCategoryPermissions
        {
            public const string View = "TransactionCategory.View";
            public const string Create = "TransactionCategory.Create";
            public const string Update = "TransactionCategory.Update";
            public const string Delete = "TransactionCategory.Delete";
          
        }

        public static class PaymentMethodPermissions
        {
            public const string View = "PaymentMethod.View";
            public const string Create = "PaymentMethod.Create";
            public const string Update = "PaymentMethod.Update";
            public const string Delete = "PaymentMethod.Delete";

        }

        public static class  TransactionRecordPermissions
        {
            public const string View = "TransactionRecord.View";
            public const string Create = "TransactionRecord.Create";
            public const string Update = "TransactionRecord.Update";
            public const string Delete = "TransactionRecord.Delete";
            public const string Export = "TransactionRecord.Export";
            public const string Approve  = "TransactionRecord.Approve";

        }

        public static class RolePermissions
        {
            public const string View = "Role.View";

        }

        public static class AuthPermissions
        {
            public const string RegisterUser = "Auth.RegisterUser";
            public const string RevokeToken = "Auth.RevokeToken";
        }


        public static class ApplicationUserPermissions
        {
            
            public const string View = "ApplicationUser.View";
            public const string Update = "ApplicationUser.Update";
        }


    }
}
