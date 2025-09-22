using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionRecordStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Execute stored procedures from embedded resources
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.PatchTransactionRecord.sql"
            );

            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.CreateTransactionRecord.sql"
            );

            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.UpdateTransactionRecord.sql"
            );

            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.DeleteTransactionRecord.sql"
            );

            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.GetAllTransactionRecords.sql"
            );

            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.GetByIdTransactionRecord.sql"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_PatchTransactionRecord");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_CreateTransactionRecord");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_UpdateTransactionRecord");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_DeleteTransactionRecord");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetAllTransactionRecords");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetByIdTransactionRecord");
        }
    }
}
