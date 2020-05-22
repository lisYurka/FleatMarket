using Microsoft.EntityFrameworkCore.Migrations;

namespace FleatMarket.Infrastructure.Migrations
{
    public partial class initc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "efa23a53-f341-45bd-915c-5c6ac88609ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "3d5b5d3c-7ada-41e0-bbb7-faff8787329a");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "37c1ed7b-4b3e-4c88-b174-f33eb2a5ad0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2def077a-8c65-41ce-a76d-3fdcb36fa9bd");
        }
    }
}
