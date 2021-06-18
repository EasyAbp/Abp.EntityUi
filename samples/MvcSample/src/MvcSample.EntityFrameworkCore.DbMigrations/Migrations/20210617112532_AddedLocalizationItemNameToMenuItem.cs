using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class AddedLocalizationItemNameToMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalizationItemName",
                table: "EasyAbpAbpEntityUiMenuItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalizationItemName",
                table: "EasyAbpAbpEntityUiMenuItems");
        }
    }
}
