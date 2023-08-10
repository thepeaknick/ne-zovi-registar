using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class User_Active_Add_Default_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "User",
                type: "tinyint(1)",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 362, DateTimeKind.Local).AddTicks(103));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 362, DateTimeKind.Local).AddTicks(145));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 362, DateTimeKind.Local).AddTicks(144));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 362, DateTimeKind.Local).AddTicks(142));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 18, 36, 15, 373, DateTimeKind.Local).AddTicks(3807), new Guid("379c4272-ffed-4b49-81fa-10e3e32eb228") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 18, 36, 15, 373, DateTimeKind.Local).AddTicks(3834), new Guid("5127fe31-38ff-44f4-af56-deda6777b25d") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 369, DateTimeKind.Local).AddTicks(563));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 369, DateTimeKind.Local).AddTicks(600));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(5132));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(5130));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(9811));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(9813));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 36, 15, 372, DateTimeKind.Local).AddTicks(9814));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "User",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 37, DateTimeKind.Local).AddTicks(4424));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 37, DateTimeKind.Local).AddTicks(4476));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 37, DateTimeKind.Local).AddTicks(4474));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 37, DateTimeKind.Local).AddTicks(4473));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 18, 32, 25, 49, DateTimeKind.Local).AddTicks(9320), new Guid("de627daa-165a-432e-a6fc-cccec0d291ae") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 18, 32, 25, 49, DateTimeKind.Local).AddTicks(9348), new Guid("edd375f6-19d7-4a57-89c9-edc17e87f883") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 44, DateTimeKind.Local).AddTicks(9272));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 44, DateTimeKind.Local).AddTicks(9324));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 48, DateTimeKind.Local).AddTicks(7716));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 48, DateTimeKind.Local).AddTicks(7757));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 48, DateTimeKind.Local).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 49, DateTimeKind.Local).AddTicks(4909));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 49, DateTimeKind.Local).AddTicks(4935));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 49, DateTimeKind.Local).AddTicks(4930));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 49, DateTimeKind.Local).AddTicks(4932));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 32, 25, 49, DateTimeKind.Local).AddTicks(4934));
        }
    }
}
