using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NeZoviReg.Migrations.Ef.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    table.PrimaryKey("PK_Permission", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuidId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompanyName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ForgotPasswordToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForgotPasswordTokenExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegNumber = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxNumber = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    table.PrimaryKey("PK_RegUser", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Jmbg = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_RegUser_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "RegUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegUserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_RegUserRole", x => new { x.RegUserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RegUserRole_RegUser_RegUserId",
                        column: x => x.RegUserId,
                        principalTable: "RegUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegUserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 2, "init", new DateTime(2023, 7, 8, 15, 28, 33, 104, DateTimeKind.Local).AddTicks(9092), null, null, "RegUsersOnly" },
                    { 4, "init", new DateTime(2023, 7, 8, 15, 28, 33, 104, DateTimeKind.Local).AddTicks(9133), null, null, "Write" },
                    { 8, "init", new DateTime(2023, 7, 8, 15, 28, 33, 104, DateTimeKind.Local).AddTicks(9131), null, null, "Delete" },
                    { 16, "init", new DateTime(2023, 7, 8, 15, 28, 33, 104, DateTimeKind.Local).AddTicks(9130), null, null, "Read" }
                });

            migrationBuilder.InsertData(
                table: "RegUser",
                columns: new[] { "Id", "Address", "CompanyName", "CreatedBy", "CreatedOn", "Email", "FirstName", "ForgotPasswordToken", "ForgotPasswordTokenExpirationTime", "GuidId", "LastName", "ModifiedBy", "ModifiedOn", "Password", "RefreshToken", "RefreshTokenExpirationTime", "RegNumber", "TaxNumber", "Username" },
                values: new object[,]
                {
                    { 1, "Palmotićeva 2", "RATEL", "init", new DateTime(2023, 7, 8, 15, 28, 33, 116, DateTimeKind.Local).AddTicks(9766), "mail@mail.com", "Ime", null, null, new Guid("4bc2c918-d059-4979-9570-68c497876930"), "Prezime", null, null, "dGVzdDEyMw==", null, null, "00000000", "000000000", "ratel" },
                    { 2, "Palmotićeva 2", "RATEL2", "init", new DateTime(2023, 7, 8, 15, 28, 33, 116, DateTimeKind.Local).AddTicks(9796), "mail2@mail.com", "Ime", null, null, new Guid("089e7b3e-756a-423b-88cf-a9603eec8717"), "Prezime", null, null, "dGVzdDEyMw==", null, null, "11111111", "111111111", "ratel2" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, "init", new DateTime(2023, 7, 8, 15, 28, 33, 115, DateTimeKind.Local).AddTicks(1684), null, null, "Admin" },
                    { 2, "init", new DateTime(2023, 7, 8, 15, 28, 33, 115, DateTimeKind.Local).AddTicks(1702), null, null, "Trgovac" },
                    { 3, "init", new DateTime(2023, 7, 8, 15, 28, 33, 115, DateTimeKind.Local).AddTicks(1700), null, null, "Obveznik" }
                });

            migrationBuilder.InsertData(
                table: "RegUserRole",
                columns: new[] { "RegUserId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, 1, "init", new DateTime(2023, 7, 8, 15, 28, 33, 111, DateTimeKind.Local).AddTicks(7628), null, null },
                    { 2, 1, "init", new DateTime(2023, 7, 8, 15, 28, 33, 111, DateTimeKind.Local).AddTicks(7673), null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[,]
                {
                    { 2, 1, "init", new DateTime(2023, 7, 8, 15, 28, 33, 116, DateTimeKind.Local).AddTicks(3161), null, null },
                    { 16, 2, "init", new DateTime(2023, 7, 8, 15, 28, 33, 116, DateTimeKind.Local).AddTicks(3183), null, null },
                    { 4, 3, "init", new DateTime(2023, 7, 8, 15, 28, 33, 116, DateTimeKind.Local).AddTicks(3178), null, null },
                    { 8, 3, "init", new DateTime(2023, 7, 8, 15, 28, 33, 116, DateTimeKind.Local).AddTicks(3180), null, null },
                    { 16, 3, "init", new DateTime(2023, 7, 8, 15, 28, 33, 116, DateTimeKind.Local).AddTicks(3181), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_CompanyName",
                table: "RegUser",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Email",
                table: "RegUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_RegNumber",
                table: "RegUser",
                column: "RegNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_TaxNumber",
                table: "RegUser",
                column: "TaxNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Username",
                table: "RegUser",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegUserRole_RoleId",
                table: "RegUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_OperatorId",
                table: "User",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegUserRole");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RegUser");
        }
    }
}
