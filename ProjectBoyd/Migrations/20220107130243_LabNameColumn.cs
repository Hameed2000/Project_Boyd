using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBoyd.Migrations
{
    public partial class LabNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LabName",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabName",
                table: "Modules");
        }
    }
}
