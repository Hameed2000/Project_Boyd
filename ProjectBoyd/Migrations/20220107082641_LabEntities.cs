using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBoyd.Migrations
{
    public partial class LabEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    LabId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabIconAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    TagsList = table.Column<int>(type: "int", nullable: false),
                    TipsList = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.LabId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabId = table.Column<int>(type: "int", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LowRange = table.Column<int>(type: "int", nullable: false),
                    HighRange = table.Column<int>(type: "int", nullable: false),
                    ServiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CDSImageAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    TipsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabId = table.Column<int>(type: "int", nullable: false),
                    TipText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbsUp = table.Column<int>(type: "int", nullable: false),
                    ThumbsDown = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.TipsId);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tasks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.WorkOrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Tips");

            migrationBuilder.DropTable(
                name: "WorkOrders");
        }
    }
}
