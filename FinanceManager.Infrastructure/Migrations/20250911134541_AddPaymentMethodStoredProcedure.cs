using FinanceManager.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentMethodStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Execute GetAll SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.GetAllPaymentMethods.sql"
            );

            // Execute GetById SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.GetPaymentMethodById.sql"
            );

            // Execute Create SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.CreatePaymentMethod.sql"
            );

            // Execute Update SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.UpdatePaymentMethod.sql"
            );

            // Execute Delete SP
            MigrationHelper.RunSqlScript(
                migrationBuilder,
                "FinanceManager.Infrastructure.Database.StoredProcedures.PaymentMethod.DeletePaymentMethod.sql"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetAllPaymentMethods");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_GetPaymentMethodById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_CreatePaymentMethod");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_UpdatePaymentMethod");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_DeletePaymentMethod");
        }
    }
}
