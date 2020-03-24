using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FleatMarket.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeclarationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclarationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Declarations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    TimeOfCreation = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DeclarationStatusId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declarations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Declarations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Declarations_DeclarationStatuses_DeclarationStatusId",
                        column: x => x.DeclarationStatusId,
                        principalTable: "DeclarationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Declarations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Путешествия" },
                    { 2, "Развлечения" },
                    { 3, "Книги" },
                    { 4, "Техника" },
                    { 5, "В дар" },
                    { 6, "Животные" }
                });

            migrationBuilder.InsertData(
                table: "DeclarationStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Открыто" },
                    { 2, "Продано" },
                    { 3, "Удалено" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EMail", "IsActive", "Name", "Password", "Phone", "RoleId", "Surname" },
                values: new object[] { 1, "User1@mail.ru", true, "Vasya", "User1", "123456789", 1, "Ivanov" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EMail", "IsActive", "Name", "Password", "Phone", "RoleId", "Surname" },
                values: new object[] { 2, "User2@mail.ru", true, "Petya", "User2", "987654321", 1, "Tushenka" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EMail", "IsActive", "Name", "Password", "Phone", "RoleId", "Surname" },
                values: new object[] { 3, "Admin@mail.ru", true, "Alesha", "Admin", "192837465", 2, "Popovich" });

            migrationBuilder.InsertData(
                table: "Declarations",
                columns: new[] { "Id", "CategoryId", "DeclarationStatusId", "Description", "TimeOfCreation", "Title", "UserId" },
                values: new object[] { 1, 1, 1, "Не упустите момент попасть на российские Мальдивы", new DateTime(2020, 3, 24, 21, 40, 38, 347, DateTimeKind.Local).AddTicks(3060), "Путевка в Челябинск", 1 });

            migrationBuilder.InsertData(
                table: "Declarations",
                columns: new[] { "Id", "CategoryId", "DeclarationStatusId", "Description", "TimeOfCreation", "Title", "UserId" },
                values: new object[] { 2, 5, 1, "Заберите кота от меня подальше", new DateTime(2020, 3, 24, 21, 40, 38, 348, DateTimeKind.Local).AddTicks(1983), "Британец короткошерстный", 2 });

            migrationBuilder.InsertData(
                table: "Declarations",
                columns: new[] { "Id", "CategoryId", "DeclarationStatusId", "Description", "TimeOfCreation", "Title", "UserId" },
                values: new object[] { 3, 3, 2, "Увлекательное путешествие в мир волшебства", new DateTime(2020, 3, 24, 21, 40, 38, 348, DateTimeKind.Local).AddTicks(2024), "Книга Гарри Поттера", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Declarations_CategoryId",
                table: "Declarations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Declarations_DeclarationStatusId",
                table: "Declarations",
                column: "DeclarationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Declarations_UserId",
                table: "Declarations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Declarations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DeclarationStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
