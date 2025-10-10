using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionRecordGetAllSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            MigrationHelper.RunSqlScript(
            migrationBuilder,
           "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.GetAllTransactionRecords.sql"
);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetAllTransactionRecords");
        }
    }
}
