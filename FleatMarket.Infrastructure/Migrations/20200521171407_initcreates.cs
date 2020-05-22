using Microsoft.EntityFrameworkCore.Migrations;

namespace FleatMarket.Infrastructure.Migrations
{
    public partial class initcreates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Image_ImageId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_ImageId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Image");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "849672c0-32ce-417b-83e8-78637f5b893e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6941c43e-ad73-43f9-96a9-511a7e43a3fb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Image",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Image_ImageId",
                table: "Image",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Image_ImageId",
                table: "Image",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
