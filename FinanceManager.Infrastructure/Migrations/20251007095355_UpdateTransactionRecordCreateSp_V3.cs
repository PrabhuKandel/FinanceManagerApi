using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionRecordCreateSp_V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Execute Create SP from embedded resource
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.CreateTransactionRecord.sql"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_CreateTransactionRecord");
        }
    }
}
