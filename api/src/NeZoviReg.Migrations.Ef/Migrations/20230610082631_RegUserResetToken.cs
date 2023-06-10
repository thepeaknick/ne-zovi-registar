using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class RegUserResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "RegUser",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForgotPasswordToken",
                table: "RegUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ForgotPasswordTokenExpirationTime",
                table: "RegUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 966, DateTimeKind.Local).AddTicks(2884));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 966, DateTimeKind.Local).AddTicks(2947));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 966, DateTimeKind.Local).AddTicks(2945));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 966, DateTimeKind.Local).AddTicks(2942));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Email", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 10, 10, 26, 30, 985, DateTimeKind.Local).AddTicks(2132), "ratel@ratel.rs", null, null, new Guid("4de0117c-e830-4295-81f1-ae81582c776c") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 975, DateTimeKind.Local).AddTicks(9944));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 983, DateTimeKind.Local).AddTicks(299));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 983, DateTimeKind.Local).AddTicks(358));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 983, DateTimeKind.Local).AddTicks(354));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 984, DateTimeKind.Local).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 984, DateTimeKind.Local).AddTicks(2139));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 984, DateTimeKind.Local).AddTicks(2129));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 984, DateTimeKind.Local).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 10, 10, 26, 30, 984, DateTimeKind.Local).AddTicks(2136));

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Email",
                table: "RegUser",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegUser_Email",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "ForgotPasswordToken",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "ForgotPasswordTokenExpirationTime",
                table: "RegUser");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3470));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3522));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3521));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3519));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 7, 17, 28, 23, 652, DateTimeKind.Local).AddTicks(363), new Guid("5175b8da-e5dc-4da1-a557-5a69e0e1b9b4") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 642, DateTimeKind.Local).AddTicks(6310));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 649, DateTimeKind.Local).AddTicks(8832));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 649, DateTimeKind.Local).AddTicks(8894));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 649, DateTimeKind.Local).AddTicks(8891));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1050));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1089));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1078));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1083));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1085));
        }
    }
}
