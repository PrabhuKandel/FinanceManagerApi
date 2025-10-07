using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActionedByApplicationuUserInTransactionRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActionedAt",
                table: "TransactionRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionedByApplicationUserId",
                table: "TransactionRecords",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecords_ActionedByApplicationUserId",
                table: "TransactionRecords",
                column: "ActionedByApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_ActionedByApplicationUserId",
                table: "TransactionRecords",
                column: "ActionedByApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_ActionedByApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRecords_ActionedByApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "ActionedAt",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "ActionedByApplicationUserId",
                table: "TransactionRecords");
        }
    }
}
