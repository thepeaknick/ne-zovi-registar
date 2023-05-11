using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "OperatorId",
                table: "User",
                type: "int",
                maxLength: 25,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 657, DateTimeKind.Local).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 657, DateTimeKind.Local).AddTicks(8754));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 657, DateTimeKind.Local).AddTicks(8751));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 657, DateTimeKind.Local).AddTicks(8749));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 657, DateTimeKind.Local).AddTicks(8753));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 5, 11, 16, 22, 38, 673, DateTimeKind.Local).AddTicks(5907), new Guid("c82f079d-fc3a-438b-a479-b2fde42a9f48") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 667, DateTimeKind.Local).AddTicks(2469));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 672, DateTimeKind.Local).AddTicks(1935));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 672, DateTimeKind.Local).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 672, DateTimeKind.Local).AddTicks(1986));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 672, DateTimeKind.Local).AddTicks(1994));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 672, DateTimeKind.Local).AddTicks(9995));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 673, DateTimeKind.Local).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 673, DateTimeKind.Local).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 673, DateTimeKind.Local).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 673, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 16, 22, 38, 673, DateTimeKind.Local).AddTicks(36));

            migrationBuilder.CreateIndex(
                name: "IX_User_OperatorId",
                table: "User",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_RegUser_OperatorId",
                table: "User",
                column: "OperatorId",
                principalTable: "RegUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_RegUser_OperatorId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_OperatorId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "User");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(177));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(222));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(219));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(217));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(220));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2023, 5, 11, 15, 51, 26, 686, DateTimeKind.Local).AddTicks(2091), new Guid("e57059c3-d4f7-46bb-88c7-f961ed11f22c") });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 680, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(446));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(484));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(482));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(486));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6920));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6941));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 2 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6942));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6937));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, 3 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6939));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                column: "CreatedOn",
                value: new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6944));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "FirstName", "Jmbg", "LastName", "ModifiedBy", "ModifiedOn", "PhoneNumber" },
                values: new object[] { 1, "test", new DateTime(2023, 5, 11, 15, 51, 26, 687, DateTimeKind.Local).AddTicks(5179), "Marko", "012345567", "Bubulj", null, null, "1234" });
        }
    }
}
