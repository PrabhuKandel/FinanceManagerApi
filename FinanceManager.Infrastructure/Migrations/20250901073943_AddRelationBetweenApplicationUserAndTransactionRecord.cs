using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenApplicationUserAndTransactionRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TransactionRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecords_ApplicationUserId",
                table: "TransactionRecords",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_ApplicationUserId",
                table: "TransactionRecords",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_ApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRecords_ApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TransactionRecords");
        }
    }
}
