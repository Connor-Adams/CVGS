using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Team6CVGS.Migrations
{
    public partial class reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    gameGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reviewDate = table.Column<DateTime>(type: "date", nullable: false),
                    reviewContent = table.Column<string>(type: "text", nullable: false),
                    reviewRaiting = table.Column<int>(type: "int", nullable: false),
                    approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_Review_Game",
                        column: x => x.gameGuid,
                        principalTable: "Game",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_Person",
                        column: x => x.userId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_gameGuid",
                table: "Review",
                column: "gameGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Review_userId",
                table: "Review",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
