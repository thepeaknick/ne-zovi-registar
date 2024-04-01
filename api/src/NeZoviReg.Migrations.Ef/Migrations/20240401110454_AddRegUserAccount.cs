using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddRegUserAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegUser_Username",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "AccessTokenExpirationTime",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "ForgotPasswordToken",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "ForgotPasswordTokenExpirationTime",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationTime",
                table: "RegUser");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "RegUser");

            migrationBuilder.AlterColumn<string>(
                name: "Jmbg",
                table: "User",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldMaxLength: 13)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegUserAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuidId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ForgotPasswordToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForgotPasswordTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Rowversion = table.Column<DateTime>(type: "datetime(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegUserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegUserAccount_RegUser_RegUserId",
                        column: x => x.RegUserId,
                        principalTable: "RegUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 858, DateTimeKind.Local).AddTicks(5404));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 858, DateTimeKind.Local).AddTicks(5462));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 858, DateTimeKind.Local).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 858, DateTimeKind.Local).AddTicks(5458));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 1, 13, 4, 53, 891, DateTimeKind.Local).AddTicks(6949), new Guid("f898edcf-8d1f-44d9-9d33-e34b4cd33ad4") });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "GuidId" },
                values: new object[] { new DateTime(2024, 4, 1, 13, 4, 53, 891, DateTimeKind.Local).AddTicks(6992), new Guid("1c177fd6-f2a3-4905-a2e3-4b11665697f8") });

            migrationBuilder.InsertData(
                table: "RegUserAccount",
                columns: new[] { "Id", "AccessTokenExpirationTime", "CreatedBy", "CreatedOn", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "ModifiedBy", "ModifiedOn", "Password", "RefreshToken", "RefreshTokenExpirationTime", "RegUserId", "Username" },
                values: new object[,]
                {
                    { 1, null, "init", new DateTime(2024, 4, 1, 13, 4, 53, 890, DateTimeKind.Local).AddTicks(7547), null, null, new Guid("2e917316-e77c-4c26-85f3-2fad71297b17"), null, null, "dGVzdDEyMw==", null, null, 1, "ratel" },
                    { 2, null, "init", new DateTime(2024, 4, 1, 13, 4, 53, 890, DateTimeKind.Local).AddTicks(7631), null, null, new Guid("c4d53306-5be2-42b0-858d-c3f229b72246"), null, null, "dGVzdDEyMw==", null, null, 2, "ratel2" }
                });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 883, DateTimeKind.Local).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 883, DateTimeKind.Local).AddTicks(638));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 889, DateTimeKind.Local).AddTicks(2494));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 889, DateTimeKind.Local).AddTicks(2558));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 889, DateTimeKind.Local).AddTicks(2555));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 890, DateTimeKind.Local).AddTicks(1486));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 890, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 890, DateTimeKind.Local).AddTicks(1514));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 890, DateTimeKind.Local).AddTicks(1516));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 4, 1, 13, 4, 53, 890, DateTimeKind.Local).AddTicks(1518));

            migrationBuilder.CreateIndex(
                name: "IX_RegUserAccount_RegUserId",
                table: "RegUserAccount",
                column: "RegUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegUserAccount_Username",
                table: "RegUserAccount",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegUserAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Jmbg",
                table: "User",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessTokenExpirationTime",
                table: "RegUser",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForgotPasswordToken",
                table: "RegUser",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ForgotPasswordTokenExpirationTime",
                table: "RegUser",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "RegUser",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "RegUser",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpirationTime",
                table: "RegUser",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "RegUser",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(1958));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(2018));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(2016));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 316, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccessTokenExpirationTime", "CreatedOn", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "Password", "RefreshToken", "RefreshTokenExpirationTime", "Username" },
                values: new object[] { null, new DateTime(2024, 1, 11, 20, 51, 24, 334, DateTimeKind.Local).AddTicks(4529), null, null, new Guid("0b86f832-d3f5-49d2-aa01-abf8c15c978f"), "dGVzdDEyMw==", null, null, "ratel" });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccessTokenExpirationTime", "CreatedOn", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "Password", "RefreshToken", "RefreshTokenExpirationTime", "Username" },
                values: new object[] { null, new DateTime(2024, 1, 11, 20, 51, 24, 334, DateTimeKind.Local).AddTicks(4582), null, null, new Guid("afbe908a-bada-4ecc-9d66-f015213ffe3b"), "dGVzdDEyMw==", null, null, "ratel2" });

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 325, DateTimeKind.Local).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "RegUserRole",
                keyColumns: new[] { "RegUserId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 325, DateTimeKind.Local).AddTicks(8792));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(2449));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(2447));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9366));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9388));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 3 },
                column: "CreatedOn",
                value: new DateTime(2024, 1, 11, 20, 51, 24, 333, DateTimeKind.Local).AddTicks(9387));

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Username",
                table: "RegUser",
                column: "Username",
                unique: true);
        }
    }
}
