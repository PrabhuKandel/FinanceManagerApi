using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCaseInsensitiveUniqueNameValidationToTransactionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("016d8792-b664-4404-8a99-42cda6770515"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("255bdc72-3652-4a60-b0c7-a7f40bd49110"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("2e475d44-5fb3-4b03-82ab-5fc3e603eba4"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("33e104b6-782a-4721-923a-f6b33daea95d"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("47009b41-77a3-4822-9718-45b974a0c093"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionCategories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("03f21fe3-a455-494e-ad12-ba7ff53bcfa8"), "Freelance or side income", "Freelance", 0 },
                    { new Guid("085fd7e2-1406-4ad1-9116-a658685426c3"), "Movies, games, hobbies", "Entertainment", 1 },
                    { new Guid("15f98ec0-123a-44e4-bc86-e6560a96d006"), "Monthly income from job", "Salary", 0 },
                    { new Guid("37ea8c7a-829e-4481-95bd-6a035833901b"), "Groceries and dining out", "Food", 1 },
                    { new Guid("c0bc0dc0-d03d-41b1-9a54-a0938ea62ecf"), "Bus, train, fuel, ride shares", "Transport", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_Name",
                table: "TransactionCategories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCategories_Name",
                table: "TransactionCategories");

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("03f21fe3-a455-494e-ad12-ba7ff53bcfa8"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("085fd7e2-1406-4ad1-9116-a658685426c3"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("15f98ec0-123a-44e4-bc86-e6560a96d006"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("37ea8c7a-829e-4481-95bd-6a035833901b"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("c0bc0dc0-d03d-41b1-9a54-a0938ea62ecf"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("016d8792-b664-4404-8a99-42cda6770515"), "Monthly income from job", "Salary", 0 },
                    { new Guid("255bdc72-3652-4a60-b0c7-a7f40bd49110"), "Movies, games, hobbies", "Entertainment", 1 },
                    { new Guid("2e475d44-5fb3-4b03-82ab-5fc3e603eba4"), "Bus, train, fuel, ride shares", "Transport", 1 },
                    { new Guid("33e104b6-782a-4721-923a-f6b33daea95d"), "Freelance or side income", "Freelance", 0 },
                    { new Guid("47009b41-77a3-4822-9718-45b974a0c093"), "Groceries and dining out", "Food", 1 }
                });
        }
    }
}
