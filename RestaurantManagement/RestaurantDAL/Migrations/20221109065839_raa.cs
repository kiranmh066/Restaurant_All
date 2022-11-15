using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantDAL.Migrations
{
    public partial class raa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Employee",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpSpeciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpPhone = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Employee", x => x.EmpId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodCost = table.Column<double>(type: "float", nullable: false),
                    FoodCuisine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Food", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HallTable",
                columns: table => new
                {
                    HallTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallTableSize = table.Column<int>(type: "int", nullable: false),
                    HallTableStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HallTable", x => x.HallTableId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallTableId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_tbl_Feedback_tbl_HallTable_HallTableId",
                        column: x => x.HallTableId,
                        principalTable: "tbl_HallTable",
                        principalColumn: "HallTableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    HallTableId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderTotal = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<bool>(type: "bit", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_tbl_Order_tbl_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "tbl_Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Order_tbl_HallTable_HallTableId",
                        column: x => x.HallTableId,
                        principalTable: "tbl_HallTable",
                        principalColumn: "HallTableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Bill",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallTableId = table.Column<int>(type: "int", nullable: false),
                    BillStatus = table.Column<bool>(type: "bit", nullable: false),
                    OrderId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Bill", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_tbl_Bill_tbl_HallTable_HallTableId",
                        column: x => x.HallTableId,
                        principalTable: "tbl_HallTable",
                        principalColumn: "HallTableId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Bill_tbl_Order_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "tbl_Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bill_HallTableId",
                table: "tbl_Bill",
                column: "HallTableId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bill_OrderId1",
                table: "tbl_Bill",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Feedback_HallTableId",
                table: "tbl_Feedback",
                column: "HallTableId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Order_FoodId",
                table: "tbl_Order",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Order_HallTableId",
                table: "tbl_Order",
                column: "HallTableId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Order_Order",
                table: "tbl_Order",
                column: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Order_tbl_Bill_Order",
                table: "tbl_Order",
                column: "Order",
                principalTable: "tbl_Bill",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Bill_tbl_HallTable_HallTableId",
                table: "tbl_Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Order_tbl_HallTable_HallTableId",
                table: "tbl_Order");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Bill_tbl_Order_OrderId1",
                table: "tbl_Bill");

            migrationBuilder.DropTable(
                name: "tbl_Admin");

            migrationBuilder.DropTable(
                name: "tbl_Employee");

            migrationBuilder.DropTable(
                name: "tbl_Feedback");

            migrationBuilder.DropTable(
                name: "tbl_HallTable");

            migrationBuilder.DropTable(
                name: "tbl_Order");

            migrationBuilder.DropTable(
                name: "tbl_Bill");

            migrationBuilder.DropTable(
                name: "tbl_Food");
        }
    }
}
