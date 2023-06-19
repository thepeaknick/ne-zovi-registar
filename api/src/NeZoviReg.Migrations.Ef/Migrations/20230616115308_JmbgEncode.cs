using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class JmbgEncode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegUserRole_RegUser_RegUserId",
                table: "RegUserRole");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 779, DateTimeKind.Local).AddTicks(275));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 779, DateTimeKind.Local).AddTicks(323));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 779, DateTimeKind.Local).AddTicks(321));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 779, DateTimeKind.Local).AddTicks(319));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Email", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 16, 13, 53, 8, 790, DateTimeKind.Local).AddTicks(3747), "markobubulj.test@gmail.com", new Guid("34d8f9e7-f9e0-42bd-9f56-2fc80fd362c6") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 785, DateTimeKind.Local).AddTicks(8761));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(3592));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(3609));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(8693));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(8688));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(8690));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 53, 8, 789, DateTimeKind.Local).AddTicks(8692));

            migrationBuilder.AddForeignKey(
                name: "FK_RegUserRole_RegUser_RegUserId",
                table: "RegUserRole",
                column: "RegUserId",
                principalTable: "RegUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegUserRole_RegUser_RegUserId",
                table: "RegUserRole");

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
                columns: new[] { "CreatedOn", "Email", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 10, 10, 26, 30, 985, DateTimeKind.Local).AddTicks(2132), "ratel@ratel.rs", new Guid("4de0117c-e830-4295-81f1-ae81582c776c") });

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

            migrationBuilder.AddForeignKey(
                name: "FK_RegUserRole_RegUser_RegUserId",
                table: "RegUserRole",
                column: "RegUserId",
                principalTable: "RegUser",
                principalColumn: "Id");
        }
    }
}
