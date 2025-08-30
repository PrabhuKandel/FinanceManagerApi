using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionModelToTransactionRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

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

            migrationBuilder.CreateTable(
                name: "TransactionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionRecords_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionRecords_TransactionCategories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecords_PaymentMethodId",
                table: "TransactionRecords",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecords_TransactionCategoryId",
                table: "TransactionRecords",
                column: "TransactionCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionRecords");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentMethodId",
                table: "Transactions",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionCategoryId",
                table: "Transactions",
                column: "TransactionCategoryId");
        }
    }
}
