using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPatchTransactionRecordStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Execute GetById SP from embedded resource
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.PatchTransactionRecord.sql"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_PatchTransactionRecord");


        }
    }
}
