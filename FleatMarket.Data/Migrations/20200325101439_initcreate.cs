using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FleatMarket.Infrastructure.Migrations
{
    public partial class initcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 13, 14, 39, 102, DateTimeKind.Local).AddTicks(8476));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 13, 14, 39, 103, DateTimeKind.Local).AddTicks(6952));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 13, 14, 39, 103, DateTimeKind.Local).AddTicks(6991));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 13, 9, 53, 95, DateTimeKind.Local).AddTicks(8834));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 13, 9, 53, 97, DateTimeKind.Local).AddTicks(1783));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeOfCreation",
                value: new DateTime(2020, 3, 25, 13, 9, 53, 97, DateTimeKind.Local).AddTicks(1840));
        }
    }
}
