using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class User_Active_Add_DefaultValue : Migration
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
                oldNullable: true,
                oldDefaultValueSql: "1");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 433, DateTimeKind.Local).AddTicks(675));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 433, DateTimeKind.Local).AddTicks(722));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 433, DateTimeKind.Local).AddTicks(721));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 433, DateTimeKind.Local).AddTicks(719));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 21, 1, 46, 444, DateTimeKind.Local).AddTicks(9991), new Guid("ab7e1704-f2c1-42ed-be53-f658ccb20977") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 21, 1, 46, 445, DateTimeKind.Local).AddTicks(20), new Guid("a8a1216f-f18b-4205-ac9f-d0bfc0e7bfc2") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 439, DateTimeKind.Local).AddTicks(5991));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 439, DateTimeKind.Local).AddTicks(6018));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 443, DateTimeKind.Local).AddTicks(6744));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 443, DateTimeKind.Local).AddTicks(6785));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 443, DateTimeKind.Local).AddTicks(6783));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 444, DateTimeKind.Local).AddTicks(4578));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 444, DateTimeKind.Local).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 444, DateTimeKind.Local).AddTicks(4597));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 444, DateTimeKind.Local).AddTicks(4599));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 21, 1, 46, 444, DateTimeKind.Local).AddTicks(4601));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "User",
                type: "tinyint(1)",
                nullable: true,
                defaultValueSql: "1",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 791, DateTimeKind.Local).AddTicks(3542));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 791, DateTimeKind.Local).AddTicks(3588));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 791, DateTimeKind.Local).AddTicks(3586));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 791, DateTimeKind.Local).AddTicks(3585));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 20, 59, 17, 804, DateTimeKind.Local).AddTicks(7379), new Guid("45ad6b5f-dc28-40bd-87c7-cd2fe1e8f447") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 8, 10, 20, 59, 17, 804, DateTimeKind.Local).AddTicks(7408), new Guid("727bb424-f5ac-420f-9a1d-a09303bdb91e") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 799, DateTimeKind.Local).AddTicks(1325));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 799, DateTimeKind.Local).AddTicks(1385));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 803, DateTimeKind.Local).AddTicks(7376));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 803, DateTimeKind.Local).AddTicks(7411));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 803, DateTimeKind.Local).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 804, DateTimeKind.Local).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 804, DateTimeKind.Local).AddTicks(2969));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 804, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 804, DateTimeKind.Local).AddTicks(2966));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 20, 59, 17, 804, DateTimeKind.Local).AddTicks(2968));
        }
    }
}
