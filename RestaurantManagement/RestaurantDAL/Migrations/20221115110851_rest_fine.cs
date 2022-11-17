using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantDAL.Migrations
{
    public partial class rest_fine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Bill_tbl_HallTable_HallTableId",
                table: "tbl_Bill");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Bill_HallTableId",
                table: "tbl_Bill");

            migrationBuilder.DropColumn(
                name: "BillStatus",
                table: "tbl_Bill");

            migrationBuilder.DropColumn(
                name: "HallTableId",
                table: "tbl_Bill");

            migrationBuilder.AddColumn<DateTime>(
                name: "BillDate",
                table: "tbl_Bill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "tbl_Bill",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "tbl_Bill",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillDate",
                table: "tbl_Bill");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "tbl_Bill");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "tbl_Bill");

            migrationBuilder.AddColumn<bool>(
                name: "BillStatus",
                table: "tbl_Bill",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HallTableId",
                table: "tbl_Bill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bill_HallTableId",
                table: "tbl_Bill",
                column: "HallTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Bill_tbl_HallTable_HallTableId",
                table: "tbl_Bill",
                column: "HallTableId",
                principalTable: "tbl_HallTable",
                principalColumn: "HallTableId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
