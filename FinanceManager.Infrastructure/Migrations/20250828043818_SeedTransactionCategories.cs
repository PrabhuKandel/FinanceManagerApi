using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTransactionCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
