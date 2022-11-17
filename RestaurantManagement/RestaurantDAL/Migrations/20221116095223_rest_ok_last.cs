using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantDAL.Migrations
{
    public partial class rest_ok_last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Bill_tbl_Order_OrderId1",
                table: "tbl_Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Order_tbl_Bill_Order",
                table: "tbl_Order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Order_Order",
                table: "tbl_Order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Bill_OrderId1",
                table: "tbl_Bill");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "tbl_Order");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "tbl_Bill");

            migrationBuilder.AddColumn<double>(
                name: "BillTotal",
                table: "tbl_Bill",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Bill_tbl_HallTable_HallTableId",
                table: "tbl_Bill");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Bill_HallTableId",
                table: "tbl_Bill");

            migrationBuilder.DropColumn(
                name: "BillTotal",
                table: "tbl_Bill");

            migrationBuilder.DropColumn(
                name: "HallTableId",
                table: "tbl_Bill");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "tbl_Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "tbl_Bill",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Order_Order",
                table: "tbl_Order",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bill_OrderId1",
                table: "tbl_Bill",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Bill_tbl_Order_OrderId1",
                table: "tbl_Bill",
                column: "OrderId1",
                principalTable: "tbl_Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Order_tbl_Bill_Order",
                table: "tbl_Order",
                column: "Order",
                principalTable: "tbl_Bill",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
