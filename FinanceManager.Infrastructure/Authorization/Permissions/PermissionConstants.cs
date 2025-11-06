using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace FinanceManager.Infrastructure.Authorization.Permissions
{
    public static  class PermissionConstants
    {
        public static class TransactionCategory
        {
            public const string View = "TransactionCategory.View";
            public const string Create = "TransactionCategory.Create";
            public const string Update = "TransactionCategory.Update";
            public const string Delete = "TransactionCategory.Delete";
          
        }

        public static class PaymentMethod
        {
            public const string View = "PaymentMethod.View";
            public const string Create = "PaymentMethod.Create";
            public const string Update = "PaymentMethod.Update";
            public const string Delete = "PaymentMethod.Delete";

        }

        public static class  TransactionRecord
        {
            public const string View = "TransactionRecord.View";
            public const string Create = "TransactionRecord.Create";
            public const string Update = "TransactionRecord.Update";
            public const string Delete = "TransactionRecord.Delete";
            public const string Export = "TransactionRecord.Export";
            public const string Approve  = "TransactionRecord.Approve";
            public const string Import = "TransactionRecord.Import";

        }

        public static class Role
        {
            public const string View = "Role.View";
            public const string Create = "Role.Create";
            public const string AssignPermissions = "Role.AssignPermissions";

        }

        public static class Permission
        {
            public const string View = "Permissions.View";

        }

        public static class Auth
        {
            public const string RegisterUser = "Auth.RegisterUser";
            public const string RevokeToken = "Auth.RevokeToken";
        }




        public static class ApplicationUser
        {
            
            public const string View = "ApplicationUser.View";
            public const string Update = "ApplicationUser.Update";
        }


        public static class Budeget
        {
            public const string Create = "Budget.Create";
        }


        public static class Report
        {
            public const string View = "Report.View";
            public const string Export = "Report.Export";
        }


    }
}
