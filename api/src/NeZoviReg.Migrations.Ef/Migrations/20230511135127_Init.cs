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
                    { 1, "init", new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(177), null, null, "All" },
                    { 2, "init", new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(222), null, null, "Write" },
                    { 3, "init", new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(219), null, null, "Delete" },
                    { 4, "init", new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(217), null, null, "Read" },
                    { 5, "init", new DateTime(2023, 5, 11, 15, 51, 26, 673, DateTimeKind.Local).AddTicks(220), null, null, "ReadAll" }
                });

            migrationBuilder.InsertData(
                table: "RegUser",
                columns: new[] { "Id", "Address", "CompanyName", "CreatedBy", "CreatedOn", "FirstName", "GuidId", "LastName", "ModifiedBy", "ModifiedOn", "Password", "RegNumber", "TaxNumber", "Username" },
                values: new object[] { 1, "Palmotićeva 2", "RATEL", "init", new DateTime(2023, 5, 11, 15, 51, 26, 686, DateTimeKind.Local).AddTicks(2091), "Ime", new Guid("e57059c3-d4f7-46bb-88c7-f961ed11f22c"), "Prezime", null, null, "dGVzdDEyMw==", "17606590", "103986571", "ratel" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(446), null, null, "Admin" },
                    { 2, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(484), null, null, "Trgovac" },
                    { 3, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(482), null, null, "Obveznik" },
                    { 4, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(486), null, null, "Potrosac" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "FirstName", "Jmbg", "LastName", "ModifiedBy", "ModifiedOn", "PhoneNumber" },
                values: new object[] { 1, "test", new DateTime(2023, 5, 11, 15, 51, 26, 687, DateTimeKind.Local).AddTicks(5179), "Marko", "012345567", "Bubulj", null, null, "1234" });

            migrationBuilder.InsertData(
                table: "RegUserRole",
                columns: new[] { "RegUserId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[] { 1, 1, "init", new DateTime(2023, 5, 11, 15, 51, 26, 680, DateTimeKind.Local).AddTicks(8855), null, null });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" },
                values: new object[,]
                {
                    { 1, 1, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6920), null, null },
                    { 4, 2, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6941), null, null },
                    { 5, 2, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6942), null, null },
                    { 2, 3, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6937), null, null },
                    { 3, 3, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6939), null, null },
                    { 4, 4, "init", new DateTime(2023, 5, 11, 15, 51, 26, 685, DateTimeKind.Local).AddTicks(6944), null, null }
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
