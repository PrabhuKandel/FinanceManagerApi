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
            var basePath = Path.Combine(AppContext.BaseDirectory, "Database", "StoredProcedures", "TransactionCategory");
            // Execute Create SP
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "CreateTransactionCategory.sql")));

            // Execute Update SP
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "UpdateTransactionCategory.sql")));

            // Execute Delete SP
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "DeleteTransactionCategory.sql")));
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
