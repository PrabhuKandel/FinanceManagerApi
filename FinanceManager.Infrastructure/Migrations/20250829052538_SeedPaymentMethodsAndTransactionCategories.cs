using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPaymentMethodsAndTransactionCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("85ff7a2d-f256-450f-8984-30d3fa36712d"), "Unified Payment Interface", true, "UPI" },
                    { new Guid("bb55349d-b1db-4b61-bfea-a6189e2290c2"), "Cash payment", true, "Cash" },
                    { new Guid("dfb8a715-d07f-47a0-b9bd-2fd00c00d0d6"), "Payment via credit card", true, "Credit Card" },
                    { new Guid("ed6a319b-f5f4-4a7c-a7e1-587b776c6a45"), "Online PayPal payment", true, "PayPal" },
                    { new Guid("f0a1537d-2cbf-4f26-9e31-4e41dc3c4803"), "Payment via bank transfer", true, "Bank Transfer" }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("4b8210ae-4815-4054-b760-8c2771ee5c82"), "Monthly income from job", "Salary", 0 },
                    { new Guid("7df6f99a-9fb9-4ad6-9f37-ca3074593d78"), "Groceries and dining out", "Food", 1 },
                    { new Guid("87e91a20-01df-4dda-847f-9ac65894bcb1"), "Freelance or side income", "Freelance", 0 },
                    { new Guid("a269d60f-f8e7-4416-af71-9ab97f2a468a"), "Movies, games, hobbies", "Entertainment", 1 },
                    { new Guid("bc5569ef-65fd-4e4d-bfbb-54c316a9a75e"), "Bus, train, fuel, ride shares", "Transport", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("85ff7a2d-f256-450f-8984-30d3fa36712d"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("bb55349d-b1db-4b61-bfea-a6189e2290c2"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("dfb8a715-d07f-47a0-b9bd-2fd00c00d0d6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("ed6a319b-f5f4-4a7c-a7e1-587b776c6a45"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("f0a1537d-2cbf-4f26-9e31-4e41dc3c4803"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("4b8210ae-4815-4054-b760-8c2771ee5c82"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("7df6f99a-9fb9-4ad6-9f37-ca3074593d78"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("87e91a20-01df-4dda-847f-9ac65894bcb1"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("a269d60f-f8e7-4416-af71-9ab97f2a468a"));

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: new Guid("bc5569ef-65fd-4e4d-bfbb-54c316a9a75e"));

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
    }
}
