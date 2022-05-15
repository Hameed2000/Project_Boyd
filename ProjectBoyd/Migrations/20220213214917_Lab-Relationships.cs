using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBoyd.Migrations
{
    public partial class LabRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagsList",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "TipsList",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "WorkOrderId",
                table: "Modules");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_LabId",
                table: "WorkOrders",
                column: "LabId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tips_LabId",
                table: "Tips",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LabId",
                table: "Tags",
                column: "LabId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Modules_LabId",
                table: "Tags",
                column: "LabId",
                principalTable: "Modules",
                principalColumn: "LabId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Modules_LabId",
                table: "Tips",
                column: "LabId",
                principalTable: "Modules",
                principalColumn: "LabId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Modules_LabId",
                table: "WorkOrders",
                column: "LabId",
                principalTable: "Modules",
                principalColumn: "LabId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Modules_LabId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Modules_LabId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Modules_LabId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_LabId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_Tips_LabId",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_Tags_LabId",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "TagsList",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipsList",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkOrderId",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
