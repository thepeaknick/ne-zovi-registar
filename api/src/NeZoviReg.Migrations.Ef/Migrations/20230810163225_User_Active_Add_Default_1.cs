using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class User_Active_Add_Default_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 703, DateTimeKind.Local).AddTicks(3868));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 703, DateTimeKind.Local).AddTicks(3911));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 703, DateTimeKind.Local).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 703, DateTimeKind.Local).AddTicks(3908));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 18, 27, 58, 714, DateTimeKind.Local).AddTicks(6866), new Guid("41083e74-f325-4cbb-932a-678b2a5203c3") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 18, 27, 58, 714, DateTimeKind.Local).AddTicks(6899), new Guid("2bc6e659-ff08-4f25-8878-0fcd9b61de94") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 710, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 710, DateTimeKind.Local).AddTicks(3243));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 713, DateTimeKind.Local).AddTicks(7772));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 713, DateTimeKind.Local).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 713, DateTimeKind.Local).AddTicks(7788));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 714, DateTimeKind.Local).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 714, DateTimeKind.Local).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 714, DateTimeKind.Local).AddTicks(2749));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 714, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 18, 27, 58, 714, DateTimeKind.Local).AddTicks(2752));
        }
    }
}
