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
            var basePath = Path.Combine(AppContext.BaseDirectory, "Database", "StoredProcedures", "PaymentMethod");
            //Execute GetAll Sp
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "GetAllPaymentMethods.sql")));

            //Execute GetById Sp
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "GetPaymentMethodById.sql")));

            // Execute Create SP
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "CreatePaymentMethod.sql")));

            // Execute Update SP
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "UpdatePaymentMethod.sql")));

            // Execute Delete SP
            migrationBuilder.Sql(File.ReadAllText(Path.Combine(basePath, "DeletePaymentMethod.sql")));
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
