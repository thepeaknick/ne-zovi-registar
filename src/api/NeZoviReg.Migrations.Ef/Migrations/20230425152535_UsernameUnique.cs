using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class UsernameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 300, DateTimeKind.Local).AddTicks(7967));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 300, DateTimeKind.Local).AddTicks(7979));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 300, DateTimeKind.Local).AddTicks(7974));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 300, DateTimeKind.Local).AddTicks(7971));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 300, DateTimeKind.Local).AddTicks(7977));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 4, 25, 17, 25, 35, 310, DateTimeKind.Local).AddTicks(4787), new Guid("8e203ee0-fbf7-4d05-b431-2cffa244ad0d") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 306, DateTimeKind.Local).AddTicks(4004));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 309, DateTimeKind.Local).AddTicks(6744));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 309, DateTimeKind.Local).AddTicks(6751));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 309, DateTimeKind.Local).AddTicks(6749));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 309, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 310, DateTimeKind.Local).AddTicks(1233));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 310, DateTimeKind.Local).AddTicks(1242));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 310, DateTimeKind.Local).AddTicks(1244));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 310, DateTimeKind.Local).AddTicks(1237));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 310, DateTimeKind.Local).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 310, DateTimeKind.Local).AddTicks(1247));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 25, 35, 311, DateTimeKind.Local).AddTicks(5160));

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Username",
                table: "RegUser",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegUser_Username",
                table: "RegUser");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 885, DateTimeKind.Local).AddTicks(295));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 885, DateTimeKind.Local).AddTicks(308));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 885, DateTimeKind.Local).AddTicks(303));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 885, DateTimeKind.Local).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 885, DateTimeKind.Local).AddTicks(305));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 4, 25, 17, 18, 53, 895, DateTimeKind.Local).AddTicks(6817), new Guid("dc9d1424-1c4c-4b71-a194-c3953c47eee0") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 891, DateTimeKind.Local).AddTicks(5932));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 894, DateTimeKind.Local).AddTicks(8721));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 894, DateTimeKind.Local).AddTicks(8728));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 894, DateTimeKind.Local).AddTicks(8725));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 894, DateTimeKind.Local).AddTicks(8731));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 895, DateTimeKind.Local).AddTicks(3276));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 895, DateTimeKind.Local).AddTicks(3285));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 895, DateTimeKind.Local).AddTicks(3287));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 895, DateTimeKind.Local).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 895, DateTimeKind.Local).AddTicks(3282));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 895, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 25, 17, 18, 53, 896, DateTimeKind.Local).AddTicks(9608));
        }
    }
}
