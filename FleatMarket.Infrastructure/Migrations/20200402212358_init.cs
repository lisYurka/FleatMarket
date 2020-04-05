using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FleatMarket.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Declarations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f09540d4-95a5-4431-af41-d5cd1271ff3b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "fe3d8555-f2dd-4e9d-9378-f7d5e8efbdb8");

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "TimeOfCreation" },
                values: new object[] { 799.99000000000001, new DateTime(2020, 4, 3, 0, 23, 46, 808, DateTimeKind.Local).AddTicks(6179) });

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "TimeOfCreation" },
                values: new object[] { 199.99000000000001, new DateTime(2020, 4, 3, 0, 23, 46, 809, DateTimeKind.Local).AddTicks(4602) });

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "TimeOfCreation" },
                values: new object[] { 49.990000000000002, new DateTime(2020, 4, 3, 0, 23, 46, 809, DateTimeKind.Local).AddTicks(4639) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Declarations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f7bbb282-8c17-41a5-a1db-e46bdb149bd2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "910d47d5-fced-4964-8da2-45a80937ff96");

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfCreation",
                value: new DateTime(2020, 4, 1, 21, 45, 38, 547, DateTimeKind.Local).AddTicks(6021));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeOfCreation",
                value: new DateTime(2020, 4, 1, 21, 45, 38, 548, DateTimeKind.Local).AddTicks(6539));

            migrationBuilder.UpdateData(
                table: "Declarations",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeOfCreation",
                value: new DateTime(2020, 4, 1, 21, 45, 38, 548, DateTimeKind.Local).AddTicks(6600));
        }
    }
}
