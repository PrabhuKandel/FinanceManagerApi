using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableTypeInUpdatedByApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_PaymentMethods_PaymentMethodId",
                table: "TransactionRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRecords_PaymentMethodId",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "TransactionRecords");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedByApplicationUserId",
                table: "TransactionRecords",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedByApplicationUserId",
                table: "TransactionRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "TransactionRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecords_PaymentMethodId",
                table: "TransactionRecords",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_PaymentMethods_PaymentMethodId",
                table: "TransactionRecords",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
