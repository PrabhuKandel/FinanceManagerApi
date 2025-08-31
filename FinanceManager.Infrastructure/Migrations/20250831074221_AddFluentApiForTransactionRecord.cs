using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFluentApiForTransactionRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_PaymentMethods_PaymentMethodId",
                table: "TransactionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_TransactionCategories_TransactionCategoryId",
                table: "TransactionRecords");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "TransactionRecords",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TransactionRecord_Amount",
                table: "TransactionRecords",
                sql: "Amount > 0.01");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_PaymentMethods_PaymentMethodId",
                table: "TransactionRecords",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_TransactionCategories_TransactionCategoryId",
                table: "TransactionRecords",
                column: "TransactionCategoryId",
                principalTable: "TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_PaymentMethods_PaymentMethodId",
                table: "TransactionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_TransactionCategories_TransactionCategoryId",
                table: "TransactionRecords");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TransactionRecord_Amount",
                table: "TransactionRecords");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "TransactionRecords",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_PaymentMethods_PaymentMethodId",
                table: "TransactionRecords",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_TransactionCategories_TransactionCategoryId",
                table: "TransactionRecords",
                column: "TransactionCategoryId",
                principalTable: "TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
