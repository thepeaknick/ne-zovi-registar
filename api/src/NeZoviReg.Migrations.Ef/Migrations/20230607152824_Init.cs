using System;
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
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowversion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowversion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowversion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Jmbg = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    OperatorId = table.Column<int>(type: "int", maxLength: 25, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowversion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "RegUserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RegUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowversion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegUserRole", x => new { x.RegUserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RegUserRole_RegUser_RegUserId",
                        column: x => x.RegUserId,
                        principalTable: "RegUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegUserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowversion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
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
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 2, "init", new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3470), null, null, "RegUsersOnly" },
                    { 4, "init", new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3522), null, null, "Write" },
                    { 8, "init", new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3521), null, null, "Delete" },
                    { 16, "init", new DateTime(2023, 6, 7, 17, 28, 23, 631, DateTimeKind.Local).AddTicks(3519), null, null, "Read" }
                });

            migrationBuilder.InsertData(
                table: "RegUser",
                columns: new[] { "Id", "Address", "CompanyName", "CreatedBy", "CreatedOn", "FirstName", "GuidId", "LastName", "ModifiedBy", "ModifiedOn", "Password", "RefreshToken", "RefreshTokenExpirationTime", "RegNumber", "TaxNumber", "Username" },
                values: new object[] { 1, "Palmotićeva 2", "RATEL", "init", new DateTime(2023, 6, 7, 17, 28, 23, 652, DateTimeKind.Local).AddTicks(363), "Ime", new Guid("5175b8da-e5dc-4da1-a557-5a69e0e1b9b4"), "Prezime", null, null, "dGVzdDEyMw==", null, null, "17606590", "103986571", "ratel" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, "init", new DateTime(2023, 6, 7, 17, 28, 23, 649, DateTimeKind.Local).AddTicks(8832), null, null, "Admin" },
                    { 2, "init", new DateTime(2023, 6, 7, 17, 28, 23, 649, DateTimeKind.Local).AddTicks(8894), null, null, "Trgovac" },
                    { 3, "init", new DateTime(2023, 6, 7, 17, 28, 23, 649, DateTimeKind.Local).AddTicks(8891), null, null, "Obveznik" }
                });

            migrationBuilder.InsertData(
                table: "RegUserRole",
                columns: new[] { "RegUserId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[] { 1, 1, "init", new DateTime(2023, 6, 7, 17, 28, 23, 642, DateTimeKind.Local).AddTicks(6310), null, null });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[,]
                {
                    { 2, 1, "init", new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1050), null, null },
                    { 16, 2, "init", new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1089), null, null },
                    { 4, 3, "init", new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1078), null, null },
                    { 8, 3, "init", new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1083), null, null },
                    { 16, 3, "init", new DateTime(2023, 6, 7, 17, 28, 23, 651, DateTimeKind.Local).AddTicks(1085), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_CompanyName",
                table: "RegUser",
                column: "CompanyName",
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
