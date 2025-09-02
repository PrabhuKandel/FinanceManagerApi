using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAndUpdatedByToTransactionRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_ApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "TransactionRecords",
                newName: "UpdatedByApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionRecords_ApplicationUserId",
                table: "TransactionRecords",
                newName: "IX_TransactionRecords_UpdatedByApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByApplicationUserId",
                table: "TransactionRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecords_CreatedByApplicationUserId",
                table: "TransactionRecords",
                column: "CreatedByApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_CreatedByApplicationUserId",
                table: "TransactionRecords",
                column: "CreatedByApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_UpdatedByApplicationUserId",
                table: "TransactionRecords",
                column: "UpdatedByApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_CreatedByApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_UpdatedByApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRecords_CreatedByApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.DropColumn(
                name: "CreatedByApplicationUserId",
                table: "TransactionRecords");

            migrationBuilder.RenameColumn(
                name: "UpdatedByApplicationUserId",
                table: "TransactionRecords",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionRecords_UpdatedByApplicationUserId",
                table: "TransactionRecords",
                newName: "IX_TransactionRecords_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecords_AspNetUsers_ApplicationUserId",
                table: "TransactionRecords",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
