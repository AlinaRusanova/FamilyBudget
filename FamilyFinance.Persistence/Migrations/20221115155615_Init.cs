using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyFinance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetType = table.Column<int>(type: "int", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinOperation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SumBudgetItem = table.Column<int>(type: "int", nullable: false),
                    SumFinOperation = table.Column<int>(type: "int", nullable: false),
                    BudgetItemId = table.Column<int>(type: "int", nullable: true),
                    FinOperationId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperations_BudgetItems_BudgetItemId",
                        column: x => x.BudgetItemId,
                        principalTable: "BudgetItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOperations_FinancialOperations_FinOperationId",
                        column: x => x.FinOperationId,
                        principalTable: "FinancialOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOperations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BudgetItems",
                columns: new[] { "Id", "BudgetType", "Item" },
                values: new object[,]
                {
                    { 1, 0, "Salary" },
                    { 2, 0, "Deposit interest" },
                    { 3, 0, "Financial aid" },
                    { 4, 1, "Insurance" },
                    { 5, 1, "Products" },
                    { 6, 1, "Clothes" },
                    { 7, 1, "Communal payments" },
                    { 8, 1, "Rent" },
                    { 9, 1, "Medicine" },
                    { 10, 1, "Auto" },
                    { 11, 1, "Kindergarten/School" },
                    { 12, 1, "Presents" },
                    { 13, 1, "Travel, vacation" },
                    { 14, 1, "Emergency expenses" },
                    { 15, 1, "Taxes" },
                    { 16, 1, "Education" },
                    { 17, 1, "Loan payment" },
                    { 18, 1, "Entertainment" }
                });

            migrationBuilder.InsertData(
                table: "FinancialOperations",
                columns: new[] { "Id", "FinOperation" },
                values: new object[,]
                {
                    { 1, "Credit" },
                    { 2, "Deposit" },
                    { 3, "Payment by instalments" },
                    { 4, "Real estate investment" },
                    { 5, "Investments in securities" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[,]
                {
                    { 1, "Peter", "Parker", new byte[] { 181, 197, 157, 35, 54, 147, 235, 37, 79, 30, 177, 87, 33, 51, 31, 223, 124, 185, 195, 167, 232, 202, 153, 152, 234, 63, 19, 47, 133, 216, 34, 65, 236, 48, 227, 140, 99, 199, 39, 230, 204, 64, 108, 48, 33, 123, 7, 208, 142, 195, 230, 80, 209, 200, 225, 15, 146, 10, 111, 12, 42, 144, 62, 65 }, new byte[] { 54, 233, 250, 249, 178, 156, 111, 210, 247, 193, 25, 81, 239, 36, 104, 88, 69, 76, 93, 242, 17, 205, 6, 178, 217, 191, 49, 15, 177, 29, 20, 56, 197, 134, 69, 29, 159, 0, 230, 192, 120, 234, 214, 51, 125, 195, 87, 140, 59, 108, 173, 81, 207, 247, 57, 77, 192, 48, 199, 199, 24, 244, 74, 187, 230, 248, 142, 246, 89, 49, 144, 45, 244, 223, 129, 36, 113, 234, 19, 202, 148, 161, 76, 126, 173, 240, 248, 78, 48, 143, 71, 78, 234, 18, 228, 165, 148, 134, 64, 76, 112, 210, 167, 34, 70, 152, 131, 115, 183, 212, 126, 144, 160, 54, 182, 247, 112, 212, 148, 115, 144, 154, 243, 106, 232, 118, 16, 250 }, "peter_parker" },
                    { 2, "Wanda", "Maximoff", new byte[] { 65, 83, 178, 29, 3, 167, 215, 69, 70, 168, 162, 200, 44, 160, 104, 216, 211, 247, 159, 97, 138, 119, 101, 226, 95, 87, 201, 112, 105, 95, 11, 136, 41, 181, 194, 248, 244, 227, 5, 227, 152, 81, 115, 19, 140, 10, 129, 227, 157, 27, 163, 163, 214, 194, 193, 231, 158, 6, 1, 139, 106, 88, 135, 121 }, new byte[] { 54, 233, 250, 249, 178, 156, 111, 210, 247, 193, 25, 81, 239, 36, 104, 88, 69, 76, 93, 242, 17, 205, 6, 178, 217, 191, 49, 15, 177, 29, 20, 56, 197, 134, 69, 29, 159, 0, 230, 192, 120, 234, 214, 51, 125, 195, 87, 140, 59, 108, 173, 81, 207, 247, 57, 77, 192, 48, 199, 199, 24, 244, 74, 187, 230, 248, 142, 246, 89, 49, 144, 45, 244, 223, 129, 36, 113, 234, 19, 202, 148, 161, 76, 126, 173, 240, 248, 78, 48, 143, 71, 78, 234, 18, 228, 165, 148, 134, 64, 76, 112, 210, 167, 34, 70, 152, 131, 115, 183, 212, 126, 144, 160, 54, 182, 247, 112, 212, 148, 115, 144, 154, 243, 106, 232, 118, 16, 250 }, "wanda_maximoff" }
                });

            migrationBuilder.InsertData(
                table: "UserOperations",
                columns: new[] { "Id", "BudgetItemId", "Date", "FinOperationId", "SumBudgetItem", "SumFinOperation", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 50000, 0, 1 },
                    { 2, 1, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 50000, 0, 1 },
                    { 3, 1, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2000, 0, 1 },
                    { 4, 7, new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2000, 0, 1 },
                    { 5, 7, new DateTime(2022, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2000, 0, 1 },
                    { 6, 7, new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2000, 0, 1 },
                    { 7, null, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0, 30000, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOperations_BudgetItemId",
                table: "UserOperations",
                column: "BudgetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperations_FinOperationId",
                table: "UserOperations",
                column: "FinOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperations_UserId",
                table: "UserOperations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOperations");

            migrationBuilder.DropTable(
                name: "BudgetItems");

            migrationBuilder.DropTable(
                name: "FinancialOperations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
