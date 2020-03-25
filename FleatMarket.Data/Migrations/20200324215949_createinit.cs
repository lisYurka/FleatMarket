using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FleatMarket.Data.Migrations
{
    public partial class createinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 0, 59, 38, 973, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 0, 59, 38, 974, DateTimeKind.Local).AddTicks(1679));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 0, 59, 38, 974, DateTimeKind.Local).AddTicks(1718));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 0, 59, 10, 163, DateTimeKind.Local).AddTicks(993));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 0, 59, 10, 164, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 0, 59, 10, 164, DateTimeKind.Local).AddTicks(2782));
        }
    }
}
