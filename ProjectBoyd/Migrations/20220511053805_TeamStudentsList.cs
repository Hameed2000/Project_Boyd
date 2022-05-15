using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBoyd.Migrations
{
    public partial class TeamStudentsList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamEntityTeamId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_TeamEntityTeamId",
                table: "Students",
                column: "TeamEntityTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teams_TeamEntityTeamId",
                table: "Students",
                column: "TeamEntityTeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teams_TeamEntityTeamId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TeamEntityTeamId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeamEntityTeamId",
                table: "Students");
        }
    }
}
