using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addrolesAndFactionrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Factions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factions_ActivityId",
                table: "Factions",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factions_Activities_ActivityId",
                table: "Factions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factions_Activities_ActivityId",
                table: "Factions");

            migrationBuilder.DropIndex(
                name: "IX_Factions_ActivityId",
                table: "Factions");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Factions");
        }
    }
}
