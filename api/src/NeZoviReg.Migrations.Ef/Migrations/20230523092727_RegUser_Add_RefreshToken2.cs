using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class RegUser_Add_RefreshToken2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "RegUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 973, DateTimeKind.Local).AddTicks(6370));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 973, DateTimeKind.Local).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 973, DateTimeKind.Local).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 973, DateTimeKind.Local).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 973, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 5, 23, 11, 27, 26, 991, DateTimeKind.Local).AddTicks(3303), new Guid("d7cba23d-cbc5-4968-b333-d693d0851722") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 982, DateTimeKind.Local).AddTicks(2447));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 989, DateTimeKind.Local).AddTicks(9143));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 989, DateTimeKind.Local).AddTicks(9204));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 989, DateTimeKind.Local).AddTicks(9201));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 989, DateTimeKind.Local).AddTicks(9207));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 990, DateTimeKind.Local).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 990, DateTimeKind.Local).AddTicks(7343));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 990, DateTimeKind.Local).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 990, DateTimeKind.Local).AddTicks(7339));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 990, DateTimeKind.Local).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 27, 26, 990, DateTimeKind.Local).AddTicks(7346));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "RegUser",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 720, DateTimeKind.Local).AddTicks(483));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 720, DateTimeKind.Local).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 720, DateTimeKind.Local).AddTicks(536));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 720, DateTimeKind.Local).AddTicks(534));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 720, DateTimeKind.Local).AddTicks(538));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 5, 23, 11, 26, 13, 737, DateTimeKind.Local).AddTicks(8034), new Guid("3ccf33ed-8a07-4d3d-b0af-62bdb9b2e4ee") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 731, DateTimeKind.Local).AddTicks(2910));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 736, DateTimeKind.Local).AddTicks(4877));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 736, DateTimeKind.Local).AddTicks(4919));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 736, DateTimeKind.Local).AddTicks(4916));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 736, DateTimeKind.Local).AddTicks(4921));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 737, DateTimeKind.Local).AddTicks(1997));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 737, DateTimeKind.Local).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 737, DateTimeKind.Local).AddTicks(2017));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 737, DateTimeKind.Local).AddTicks(2012));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 737, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 23, 11, 26, 13, 737, DateTimeKind.Local).AddTicks(2035));
        }
    }
}
