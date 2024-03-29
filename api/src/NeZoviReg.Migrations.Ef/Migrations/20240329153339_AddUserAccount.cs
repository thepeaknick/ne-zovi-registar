using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Jmbg",
                table: "User",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldMaxLength: 13)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ForgotPasswordToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForgotPasswordTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Rowversion = table.Column<DateTime>(type: "datetime(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccount_RegUser_RegUserId",
                        column: x => x.RegUserId,
                        principalTable: "RegUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 67, DateTimeKind.Local).AddTicks(9535));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 67, DateTimeKind.Local).AddTicks(9587));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 67, DateTimeKind.Local).AddTicks(9585));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 67, DateTimeKind.Local).AddTicks(9583));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 3, 29, 16, 33, 39, 90, DateTimeKind.Local).AddTicks(4508), new Guid("38399d51-fc53-441d-b25d-08799bebf6ca") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 3, 29, 16, 33, 39, 90, DateTimeKind.Local).AddTicks(4540), new Guid("a757d1b0-f60d-4063-975b-6b61510ed970") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 81, DateTimeKind.Local).AddTicks(7812));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 81, DateTimeKind.Local).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 88, DateTimeKind.Local).AddTicks(4507));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 88, DateTimeKind.Local).AddTicks(4551));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 88, DateTimeKind.Local).AddTicks(4549));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 89, DateTimeKind.Local).AddTicks(1704));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 89, DateTimeKind.Local).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 89, DateTimeKind.Local).AddTicks(1725));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 89, DateTimeKind.Local).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 33, 39, 89, DateTimeKind.Local).AddTicks(1729));

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_RegUserId",
                table: "UserAccount",
                column: "RegUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Username",
                table: "UserAccount",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Jmbg",
                table: "User",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(1958));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(2018));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(2016));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 1, 11, 20, 51, 24, 334, DateTimeKind.Local).AddTicks(4529), new Guid("0b86f832-d3f5-49d2-aa01-abf8c15c978f") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 1, 11, 20, 51, 24, 334, DateTimeKind.Local).AddTicks(4582), new Guid("afbe908a-bada-4ecc-9d66-f015213ffe3b") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 325, DateTimeKind.Local).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 325, DateTimeKind.Local).AddTicks(8792));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(2449));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(2447));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9366));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9388));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9387));
        }
    }
}
