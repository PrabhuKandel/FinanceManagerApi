using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrateAllSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Execute stored procedures for TransactionRecord
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
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionRecord.PatchTransactionRecord.sql"
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

            MigrationHelper.RunSqlScript(
            migrationBuilder,
            "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionCategory.DeleteTransactionCategory.sql"
            );

            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionCategory.UpdateTransactionCategory.sql"
            );

            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.TransactionCategory.CreateTransactionCategory.sql"
            );

            MigrationHelper.RunSqlScript(
           migrationBuilder,
           "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.CreatePaymentMethod.sql"
            );

            MigrationHelper.RunSqlScript(
           migrationBuilder,
           "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.DeletePaymentMethod.sql"
            );

            MigrationHelper.RunSqlScript(
           migrationBuilder,
           "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.GetAllPaymentMethods.sql"
            );

            MigrationHelper.RunSqlScript(
           migrationBuilder,
           "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.GetPaymentMethodById.sql"
            );

            MigrationHelper.RunSqlScript(
           migrationBuilder,
           "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.UpdatePaymentMethod.sql"
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
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_DeleteTransactionCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_UpdateTransactionCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_CreateTransactionCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_CreatePaymentMethod");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_DeletePaymentMethod");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetAllPaymentMethods");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetPaymentMethodById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_UpdatePaymentMethod");



        }
    }
}
