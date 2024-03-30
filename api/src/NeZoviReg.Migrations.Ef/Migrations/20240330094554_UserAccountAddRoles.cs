using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class UserAccountAddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccountRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_UserAccountRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserAccountRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAccountRole_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 312, DateTimeKind.Local).AddTicks(7873));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 312, DateTimeKind.Local).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 312, DateTimeKind.Local).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 312, DateTimeKind.Local).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 3, 30, 10, 45, 54, 334, DateTimeKind.Local).AddTicks(8919), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 3, 30, 10, 45, 54, 334, DateTimeKind.Local).AddTicks(8966), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 327, DateTimeKind.Local).AddTicks(3726));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 327, DateTimeKind.Local).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(687));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(685));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(7945));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(7939));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 10, 45, 54, 332, DateTimeKind.Local).AddTicks(7943));

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "Id", "AccessTokenExpirationTime", "CreatedBy", "CreatedOn", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "ModifiedBy", "ModifiedOn", "Password", "RefreshToken", "RefreshTokenExpirationTime", "RegUserId", "Username" },
                values: new object[,]
                {
                    { 1, null, "init", new DateTime(2024, 3, 30, 10, 45, 54, 333, DateTimeKind.Local).AddTicks(2497), null, null, null, null, "dGVzdDEyMw==", null, null, 1, "ratel" },
                    { 2, null, "init", new DateTime(2024, 3, 30, 10, 45, 54, 333, DateTimeKind.Local).AddTicks(2523), null, null, null, null, "dGVzdDEyMw==", null, null, 2, "ratel2" }
                });

            migrationBuilder.InsertData(
                table: "UserAccountRole",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, 1, "init", new DateTime(2024, 3, 30, 10, 45, 54, 334, DateTimeKind.Local).AddTicks(1335), null, null },
                    { 1, 2, "init", new DateTime(2024, 3, 30, 10, 45, 54, 334, DateTimeKind.Local).AddTicks(1364), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountRole_RoleId",
                table: "UserAccountRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccountRole");

            migrationBuilder.DeleteData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: 2);

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
        }
    }
}
