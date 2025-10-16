
namespace FinanceManager.Infrastructure.Authorization
{
    public static  class PermissionConstants
    {
        public static class TransactionCategoryPermissions
        {
            public const string ViewOwn = "TransactionCategory.ViewOwn";
            public const string ViewAll = "TransactionCategory.ViewAll";
            public const string Create = "TransactionCategory.Create";
            public const string UpdateOwn = "TransactionCategory.UpdateOwn";
            public const string UpdateAll = "TransactionCategory.UpdateAll";
            public const string DeleteOwn = "TransactionCategory.DeleteOwn";
            public const string DeleteAll = "TransactionCategory.DeleteAll";
        }

    }
}
