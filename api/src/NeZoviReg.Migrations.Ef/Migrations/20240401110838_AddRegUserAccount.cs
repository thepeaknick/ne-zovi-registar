using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NeZoviReg.Migrations.Ef.Extensions;

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
            
            migrationBuilder.InsertData(
                table: "RegUserAccount",
                columns: new[] { "Id", "AccessTokenExpirationTime", "CreatedBy", "CreatedOn", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "ModifiedBy", "ModifiedOn", "Password", "RefreshToken", "RefreshTokenExpirationTime", "RegUserId", "Username" },
                values: new object[,]
                {
                    { 1, null, "init", new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(7138), null, null, new Guid("607be7dc-d827-4548-930c-00ef23ababdf"), null, null, "dGVzdDEyMw==", null, null, 1, "ratel" },
                    { 2, null, "init", new DateTime(2024, 4, 1, 13, 8, 38, 659, DateTimeKind.Local).AddTicks(7164), null, null, new Guid("037316a2-aed0-43d8-b201-acd10d9c398a"), null, null, "dGVzdDEyMw==", null, null, 2, "ratel2" }
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_RegUserAccount_RegUserId",
                table: "RegUserAccount",
                column: "RegUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegUserAccount_Username",
                table: "RegUserAccount",
                column: "Username",
                unique: true);
            

            migrationBuilder.RunFile("DataMigrations/MigrateRegUserAccountData.sql");
            
            
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegUserAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Jmbg",
                table: "User",
                type: "varchar(50)",
                maxLength: 50,
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

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Username",
                table: "RegUser",
                column: "Username",
                unique: true);
        }
    }
}
