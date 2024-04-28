using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddRegUserAccountFirsLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "RegUserAccount",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "RegUserAccount",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "RegUserAccount");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "RegUserAccount");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 646, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 646, DateTimeKind.Local).AddTicks(5267));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 646, DateTimeKind.Local).AddTicks(5265));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 646, DateTimeKind.Local).AddTicks(5263));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 1, 13, 8, 38, 660, DateTimeKind.Local).AddTicks(1576), new Guid("e52af2ae-2935-42b4-b6b2-6dc3be9e085e") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 1, 13, 8, 38, 660, DateTimeKind.Local).AddTicks(1614), new Guid("a189804e-2247-4c88-9713-f88f84f504e6") });

            migrationBuilder.UpdateData(
                table: "RegUserAccount",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId", "Password" },
                values: new object[] { new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(7138), new Guid("607be7dc-d827-4548-930c-00ef23ababdf"), "dGVzdDEyMw==" });

            migrationBuilder.UpdateData(
                table: "RegUserAccount",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId", "Password" },
                values: new object[] { new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(7164), new Guid("037316a2-aed0-43d8-b201-acd10d9c398a"), "dGVzdDEyMw==" });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 655, DateTimeKind.Local).AddTicks(4994));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 655, DateTimeKind.Local).AddTicks(5028));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 658, DateTimeKind.Local).AddTicks(8936));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 658, DateTimeKind.Local).AddTicks(8960));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 658, DateTimeKind.Local).AddTicks(8958));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(3616));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(3634));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(3629));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(3631));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(3632));
        }
    }
}
