using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPaymentMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Description",
                table: "TransactionCategories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PaymentMethods",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("4d7c3391-c38e-4fa3-a548-5c26e57553ed"), "Payment via bank transfer", true, "Bank Transfer" },
                    { new Guid("4dcc53f8-178f-49b1-81af-c9ea050bc63f"), "Online PayPal payment", true, "PayPal" },
                    { new Guid("b132f9bb-48fa-441d-b841-933f838f6623"), "Unified Payment Interface", true, "UPI" },
                    { new Guid("b4c0e23b-833e-412f-a22f-e6b566222ce9"), "Payment via credit card", true, "Credit Card" },
                    { new Guid("f25d3143-51a5-4850-973a-7b22d3ede66d"), "Cash payment", true, "Cash" }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("5f67fa5e-8237-4edb-9e3e-bf5552d3c6b2"), "Bus, train, fuel, ride shares", "Transport", 1 },
                    { new Guid("75b4678b-4dc8-4e72-b40d-f27cfaa10800"), "Freelance or side income", "Freelance", 0 },
                    { new Guid("bd1d75b7-0cf9-48b6-a12e-fff664a9742a"), "Movies, games, hobbies", "Entertainment", 1 },
                    { new Guid("cd6a376e-01bd-49c4-8e7a-f3028fbbd919"), "Monthly income from job", "Salary", 0 },
                    { new Guid("ea45cab2-1a38-4d85-8d48-68d193d9be33"), "Groceries and dining out", "Food", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("4d7c3391-c38e-4fa3-a548-5c26e57553ed"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("4dcc53f8-178f-49b1-81af-c9ea050bc63f"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("b132f9bb-48fa-441d-b841-933f838f6623"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("b4c0e23b-833e-412f-a22f-e6b566222ce9"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("f25d3143-51a5-4850-973a-7b22d3ede66d"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("5f67fa5e-8237-4edb-9e3e-bf5552d3c6b2"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("75b4678b-4dc8-4e72-b40d-f27cfaa10800"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("bd1d75b7-0cf9-48b6-a12e-fff664a9742a"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("cd6a376e-01bd-49c4-8e7a-f3028fbbd919"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("ea45cab2-1a38-4d85-8d48-68d193d9be33"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TransactionCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

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
        }
    }
}
