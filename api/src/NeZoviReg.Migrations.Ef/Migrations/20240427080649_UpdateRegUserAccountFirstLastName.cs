using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegUserAccountFirstLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 324, DateTimeKind.Local).AddTicks(9687));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 324, DateTimeKind.Local).AddTicks(9733));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 324, DateTimeKind.Local).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 324, DateTimeKind.Local).AddTicks(9729));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 6, 49, 338, DateTimeKind.Local).AddTicks(1776), new Guid("5454a379-c28b-4882-a92e-cfa8681eb53d") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 6, 49, 338, DateTimeKind.Local).AddTicks(1809), new Guid("ec379607-0abd-448e-87a2-0f1f6ebf342c") });

            migrationBuilder.UpdateData(
                table: "RegUserAccount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "FirstName", "GuidId", "LastName", "Password" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 6, 49, 333, DateTimeKind.Local).AddTicks(114), "admin", new Guid("1b9cd56e-38a3-4265-a8a4-c5f6236b9fab"), "ratel", "dGVzdDEyMw==" });

            migrationBuilder.UpdateData(
                table: "RegUserAccount",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "FirstName", "GuidId", "LastName", "Password" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 6, 49, 333, DateTimeKind.Local).AddTicks(145), "admin", new Guid("50632d4c-cc03-4a66-8f97-4896327a32b6"), "ratel", "dGVzdDEyMw==" });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 333, DateTimeKind.Local).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 333, DateTimeKind.Local).AddTicks(6588));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(57));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(81));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(72));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(4716));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(4735));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(4730));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(4732));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 6, 49, 337, DateTimeKind.Local).AddTicks(4733));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 607, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 607, DateTimeKind.Local).AddTicks(4737));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 607, DateTimeKind.Local).AddTicks(4735));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 607, DateTimeKind.Local).AddTicks(4734));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 3, 49, 621, DateTimeKind.Local).AddTicks(8381), new Guid("da202ff6-98fc-4599-a470-d7d3768f3776") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 3, 49, 621, DateTimeKind.Local).AddTicks(8418), new Guid("89dbd82d-1a7e-41a9-86e7-db0cf7aedc4a") });

            migrationBuilder.UpdateData(
                table: "RegUserAccount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "FirstName", "GuidId", "LastName", "Password" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 3, 49, 615, DateTimeKind.Local).AddTicks(8915), "test123", new Guid("2795d5ab-1b46-493e-8aba-baacd12d04cb"), "admin", "cmF0ZWw=" });

            migrationBuilder.UpdateData(
                table: "RegUserAccount",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "FirstName", "GuidId", "LastName", "Password" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 3, 49, 615, DateTimeKind.Local).AddTicks(8961), "test123", new Guid("a4837ef9-309d-4122-b8d6-fb235f6533fe"), "admin", "cmF0ZWw=" });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 616, DateTimeKind.Local).AddTicks(5807));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 616, DateTimeKind.Local).AddTicks(5835));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 620, DateTimeKind.Local).AddTicks(7129));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 620, DateTimeKind.Local).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 620, DateTimeKind.Local).AddTicks(7167));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 621, DateTimeKind.Local).AddTicks(3291));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 621, DateTimeKind.Local).AddTicks(3317));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 621, DateTimeKind.Local).AddTicks(3311));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 621, DateTimeKind.Local).AddTicks(3313));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 27, 10, 3, 49, 621, DateTimeKind.Local).AddTicks(3315));
        }
    }
}
