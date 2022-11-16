using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantDAL.Migrations
{
    public partial class res : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Cook",
                columns: table => new
                {
                    CookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Speciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CookStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Cook", x => x.CookId);
                    table.ForeignKey(
                        name: "FK_tbl_Cook_tbl_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "tbl_Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Cook_FoodId",
                table: "tbl_Cook",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Cook");
        }
    }
}
