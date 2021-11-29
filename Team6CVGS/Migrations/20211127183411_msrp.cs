using Microsoft.EntityFrameworkCore.Migrations;

namespace Team6CVGS.Migrations
{
    public partial class msrp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MSRP",
                table: "Game",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MSRP",
                table: "Game");
        }
    }
}
