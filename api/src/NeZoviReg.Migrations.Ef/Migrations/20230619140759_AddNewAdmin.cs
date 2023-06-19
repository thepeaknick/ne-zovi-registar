using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedOn", "Email", "GuidId", "RegNumber", "TaxNumber" },
                values: new object[] { new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(8739), "mail@mail.com", new Guid("da99c1d7-b3e5-4f98-bc09-1a3c36cd90e5"), "00000000", "000000000" });

            migrationBuilder.InsertData(
                table: "RegUser",
                columns: new[] { "Id", "Address", "CompanyName", "CreatedBy", "CreatedOn", "Email", "FirstName", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "LastName", "ModifiedBy", "ModifiedOn", "Password", "RefreshToken", "RefreshTokenExpirationTime", "RegNumber", "TaxNumber", "Username" },
                values: new object[] { 2, "Palmotićeva 2", "RATEL2", "init", new DateTime(2023, 6, 19, 16, 7, 59, 108, DateTimeKind.Local).AddTicks(8783), "mail2@mail.com", "Ime", null, null, new Guid("bc6307a4-746b-4bca-aae4-cebcd32c09d7"), "Prezime", null, null, "dGVzdDEyMw==", null, null, "11111111", "111111111", "ratel2" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2);

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
                columns: new[] { "CreatedOn", "Email", "GuidId", "RegNumber", "TaxNumber" },
                values: new object[] { new DateTime(2023, 6, 16, 18, 42, 9, 758, DateTimeKind.Local).AddTicks(2733), "markobubulj.test@gmail.com", new Guid("39cf5d8d-ea80-4e9e-a561-1e2b348d895e"), "17606590", "103986571" });

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
        }
    }
}
