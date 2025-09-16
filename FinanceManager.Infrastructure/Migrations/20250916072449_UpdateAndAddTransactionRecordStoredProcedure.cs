using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAndAddTransactionRecordStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(@"..\FinanceManager.Infrastructure\Database\StoredProcedures\TransactionRecord\PatchTransactionRecord.sql"));
            migrationBuilder.Sql(File.ReadAllText(@"..\FinanceManager.Infrastructure\Database\StoredProcedures\TransactionRecord\CreateTransactionRecord.sql"));
            migrationBuilder.Sql(File.ReadAllText(@"..\FinanceManager.Infrastructure\Database\StoredProcedures\TransactionRecord\UpdateTransactionRecord.sql"));
            migrationBuilder.Sql(File.ReadAllText(@"..\FinanceManager.Infrastructure\Database\StoredProcedures\TransactionRecord\DeleteTransactionRecord.sql"));
            migrationBuilder.Sql(File.ReadAllText(@"..\FinanceManager.Infrastructure\Database\StoredProcedures\TransactionRecord\GetAllTransactionRecords.sql"));
            migrationBuilder.Sql(File.ReadAllText(@"..\FinanceManager.Infrastructure\Database\StoredProcedures\TransactionRecord\GetByIdTransactionRecord.sql"));
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
