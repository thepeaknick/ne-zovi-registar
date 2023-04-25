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
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ThumbPrint = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowversion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
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
                    { 1, "test", new DateTime(2023, 4, 26, 1, 2, 53, 103, DateTimeKind.Local).AddTicks(8379), null, null, "All" },
                    { 2, "test", new DateTime(2023, 4, 26, 1, 2, 53, 103, DateTimeKind.Local).AddTicks(8393), null, null, "Write" },
                    { 3, "test", new DateTime(2023, 4, 26, 1, 2, 53, 103, DateTimeKind.Local).AddTicks(8387), null, null, "Delete" },
                    { 4, "test", new DateTime(2023, 4, 26, 1, 2, 53, 103, DateTimeKind.Local).AddTicks(8383), null, null, "Read" },
                    { 5, "test", new DateTime(2023, 4, 26, 1, 2, 53, 103, DateTimeKind.Local).AddTicks(8390), null, null, "ReadAll" }
                });

            migrationBuilder.InsertData(
                table: "RegUser",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Email", "FirstName", "GuidId", "LastName", "ModifiedBy", "ModifiedOn", "Password", "ThumbPrint", "Username" },
                values: new object[] { 1, "test", new DateTime(2023, 4, 26, 1, 2, 53, 115, DateTimeKind.Local).AddTicks(1221), "petar.petrovic@mts.rs", "Petar", new Guid("2fc74647-629d-4499-a94b-4403a53ae846"), "Petrovic", null, null, "dGVzdDEyMw==", null, "pPetrovic" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(1885), null, null, "Admin" },
                    { 2, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(1893), null, null, "Trgovac" },
                    { 3, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(1890), null, null, "Obveznik" },
                    { 4, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(1896), null, null, "Potrosac" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "FirstName", "Jmbg", "LastName", "ModifiedBy", "ModifiedOn", "PhoneNumber" },
                values: new object[] { 1, "test", new DateTime(2023, 4, 26, 1, 2, 53, 116, DateTimeKind.Local).AddTicks(4204), "Marko", "012345567", "Bubulj", null, null, "1234" });

            migrationBuilder.InsertData(
                table: "RegUserRole",
                columns: new[] { "RegUserId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[] { 1, 1, "test", new DateTime(2023, 4, 26, 1, 2, 53, 110, DateTimeKind.Local).AddTicks(4051), null, null });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, 1, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(7299), null, null },
                    { 4, 2, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(7309), null, null },
                    { 5, 2, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(7312), null, null },
                    { 2, 3, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(7304), null, null },
                    { 3, 3, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(7306), null, null },
                    { 4, 4, "test", new DateTime(2023, 4, 26, 1, 2, 53, 114, DateTimeKind.Local).AddTicks(7314), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegUser_Email",
                table: "RegUser",
                column: "Email",
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
                name: "RegUser");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
