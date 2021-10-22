using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class AddedAbpDynamicMenuModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpAbpEntityUiMenuItems");

            migrationBuilder.CreateTable(
                name: "EasyAbpAbpDynamicMenuMenuItems",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlMvc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlBlazor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlAngular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LResourceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LResourceTypeAssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpAbpDynamicMenuMenuItems", x => x.Name);
                    table.ForeignKey(
                        name: "FK_EasyAbpAbpDynamicMenuMenuItems_EasyAbpAbpDynamicMenuMenuItems_ParentName",
                        column: x => x.ParentName,
                        principalTable: "EasyAbpAbpDynamicMenuMenuItems",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpAbpDynamicMenuMenuItems_ParentName",
                table: "EasyAbpAbpDynamicMenuMenuItems",
                column: "ParentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpAbpDynamicMenuMenuItems");

            migrationBuilder.CreateTable(
                name: "EasyAbpAbpEntityUiMenuItems",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalizationItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpAbpEntityUiMenuItems", x => x.Name);
                    table.ForeignKey(
                        name: "FK_EasyAbpAbpEntityUiMenuItems_EasyAbpAbpEntityUiMenuItems_ParentName",
                        column: x => x.ParentName,
                        principalTable: "EasyAbpAbpEntityUiMenuItems",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpAbpEntityUiMenuItems_ParentName",
                table: "EasyAbpAbpEntityUiMenuItems",
                column: "ParentName");
        }
    }
}
