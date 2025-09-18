using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionCategoryStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Execute Create SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionCategory.CreateTransactionCategory.sql"
            );
            // Execute Update SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionCategory.UpdateTransactionCategory.sql"
            );

            // Execute Delete SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionCategory.DeleteTransactionCategory.sql"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_CreateTransactionCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_UpdateTransactionCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_DeleteTransactionCategory");

        }
    }
}
