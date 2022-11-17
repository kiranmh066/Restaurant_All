using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantDAL.Migrations
{
    public partial class rest_last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_AssignWork",
                columns: table => new
                {
                    AssignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AssignWork", x => x.AssignId);
                    table.ForeignKey(
                        name: "FK_tbl_AssignWork_tbl_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "tbl_Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_AssignWork_tbl_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tbl_Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AssignWork_EmpId",
                table: "tbl_AssignWork",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AssignWork_OrderId",
                table: "tbl_AssignWork",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AssignWork");
        }
    }
}
