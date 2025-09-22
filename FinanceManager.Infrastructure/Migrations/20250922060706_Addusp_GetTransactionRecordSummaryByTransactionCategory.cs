using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addusp_GetTransactionRecordSummaryByTransactionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            MigrationHelper.RunSqlScript(
      migrationBuilder,
      "FinanceManager.Infrastructure.Database.StoredProcedures.Reports.GetTransactionRecordSummaryByTransactionCategory.sql"
  );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetTransactionRecordSummaryByTransactionCategory");
        }
    }
}
