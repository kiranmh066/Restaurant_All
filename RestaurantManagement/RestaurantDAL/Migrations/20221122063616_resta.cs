using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantDAL.Migrations
{
    public partial class resta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WorkStatus",
                table: "tbl_AssignWork",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkStatus",
                table: "tbl_AssignWork");
        }
    }
}
