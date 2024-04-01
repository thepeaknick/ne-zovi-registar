using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NeZoviReg.Migrations.Ef.Extensions;

#nullable disable

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnsFromRegUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunFile($"DataMigrations/MigrateUserAccountData.sql");
            
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
                values: new object[] { null, new DateTime(2024, 3, 31, 21, 16, 35, 893, DateTimeKind.Local).AddTicks(3291), null, null, new Guid("3e8d9bcb-1c57-40f4-9ec6-e40bade5b026"), "dGVzdDEyMw==", null, null, "ratel" });

            migrationBuilder.UpdateData(
                table: "RegUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccessTokenExpirationTime", "CreatedOn", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "Password", "RefreshToken", "RefreshTokenExpirationTime", "Username" },
                values: new object[] { null, new DateTime(2024, 3, 31, 21, 16, 35, 893, DateTimeKind.Local).AddTicks(3340), null, null, new Guid("fa7ac5ed-3c76-4584-b8da-3a63e235d6ae"), "dGVzdDEyMw==", null, null, "ratel2" });

           
            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Username",
                table: "RegUser",
                column: "Username",
                unique: true);
        }
    }
}
