using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class RegUserRoleForRatel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 55, DateTimeKind.Local).AddTicks(3269));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 55, DateTimeKind.Local).AddTicks(3342));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 55, DateTimeKind.Local).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 55, DateTimeKind.Local).AddTicks(3336));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 7, 2, 12, 46, 58, 68, DateTimeKind.Local).AddTicks(3794), new Guid("83942d49-1f69-44cd-993d-b194b97a5ac9") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 7, 2, 12, 46, 58, 68, DateTimeKind.Local).AddTicks(3829), new Guid("c380b69f-daaa-4fd2-a5a9-7a762b98686a") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 63, DateTimeKind.Local).AddTicks(6165));

            migrationBuilder.InsertData(
                table: "RegUserRole",
                columns: new[] { "RegUserId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[] { 2, 1, "init", new DateTime(2023, 7, 2, 12, 46, 58, 63, DateTimeKind.Local).AddTicks(6227), null, null });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(2267));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(2301));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(2299));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(7907));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(7910));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 7, 2, 12, 46, 58, 67, DateTimeKind.Local).AddTicks(7911));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 90, DateTimeKind.Local).AddTicks(6605));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 90, DateTimeKind.Local).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 90, DateTimeKind.Local).AddTicks(6659));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 90, DateTimeKind.Local).AddTicks(6657));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(8739), new Guid("da99c1d7-b3e5-4f98-bc09-1a3c36cd90e5") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(8783), new Guid("bc6307a4-746b-4bca-aae4-cebcd32c09d7") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 101, DateTimeKind.Local).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 107, DateTimeKind.Local).AddTicks(1901));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 107, DateTimeKind.Local).AddTicks(1939));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 107, DateTimeKind.Local).AddTicks(1937));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(482));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(473));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(478));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(480));
        }
    }
}
