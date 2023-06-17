using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class PhoneNumberUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 746, DateTimeKind.Local).AddTicks(4203));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 746, DateTimeKind.Local).AddTicks(4245));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 746, DateTimeKind.Local).AddTicks(4243));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 746, DateTimeKind.Local).AddTicks(4242));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 16, 18, 42, 9, 758, DateTimeKind.Local).AddTicks(2733), new Guid("39cf5d8d-ea80-4e9e-a561-1e2b348d895e") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 753, DateTimeKind.Local).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(1384));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(1402));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(6957));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(6977));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(6974));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 18, 42, 9, 757, DateTimeKind.Local).AddTicks(6975));

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_PhoneNumber",
                table: "User");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 353, DateTimeKind.Local).AddTicks(3765));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 353, DateTimeKind.Local).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 353, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 353, DateTimeKind.Local).AddTicks(3835));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 16, 13, 55, 57, 368, DateTimeKind.Local).AddTicks(3056), new Guid("5df02f3f-340f-431f-8593-00014b456408") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 362, DateTimeKind.Local).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 366, DateTimeKind.Local).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 366, DateTimeKind.Local).AddTicks(9563));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 366, DateTimeKind.Local).AddTicks(9560));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 367, DateTimeKind.Local).AddTicks(5843));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 367, DateTimeKind.Local).AddTicks(5867));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 367, DateTimeKind.Local).AddTicks(5861));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 367, DateTimeKind.Local).AddTicks(5863));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 16, 13, 55, 57, 367, DateTimeKind.Local).AddTicks(5865));
        }
    }
}
