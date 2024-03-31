using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuidId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(88)", maxLength: 88, nullable: false)
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
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccount_RegUser_RegUserId",
                        column: x => x.RegUserId,
                        principalTable: "RegUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAccountRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_UserAccountRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserAccountRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAccountRole_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "Id", "AccessTokenExpirationTime", "CreatedBy", "CreatedOn", "Email", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "ModifiedBy", "ModifiedOn", "Password", "RefreshToken", "RefreshTokenExpirationTime", "RegUserId", "Username" },
                values: new object[,]
                {
                    { 1, null, "init", new DateTime(2024, 3, 31, 14, 8, 3, 969, DateTimeKind.Local).AddTicks(8126), "mail@mail.com", null, null, new Guid("17180e40-4fee-44ed-bbbb-5402b21a0750"), null, null, "dGVzdDEyMw==", null, null, 1, "ratel" },
                    { 2, null, "init", new DateTime(2024, 3, 31, 14, 8, 3, 969, DateTimeKind.Local).AddTicks(8157), "mail@mail.com", null, null, new Guid("4284255e-e5aa-4f6e-9b7c-98f00c47c8b1"), null, null, "dGVzdDEyMw==", null, null, 2, "ratel2" }
                });

            migrationBuilder.InsertData(
                table: "UserAccountRole",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, 1, "init", new DateTime(2024, 3, 31, 14, 8, 3, 970, DateTimeKind.Local).AddTicks(7343), null, null },
                    { 1, 2, "init", new DateTime(2024, 3, 31, 14, 8, 3, 970, DateTimeKind.Local).AddTicks(7371), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_RegUserId",
                table: "UserAccount",
                column: "RegUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Username",
                table: "UserAccount",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountRole_RoleId",
                table: "UserAccountRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccountRole");

            migrationBuilder.DropTable(
                name: "UserAccount");
        }
    }
}
