using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantDAL.Migrations
{
    public partial class resu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpId",
                table: "tbl_Cook",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Cook_EmpId",
                table: "tbl_Cook",
                column: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Cook_tbl_Employee_EmpId",
                table: "tbl_Cook",
                column: "EmpId",
                principalTable: "tbl_Employee",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Cook_tbl_Employee_EmpId",
                table: "tbl_Cook");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Cook_EmpId",
                table: "tbl_Cook");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "tbl_Cook");
        }
    }
}
