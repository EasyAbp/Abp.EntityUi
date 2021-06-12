using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class AddedLResourcePropertiesToModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LResourceTypeAssemblyName",
                table: "EasyAbpAbpEntityUiModules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LResourceTypeName",
                table: "EasyAbpAbpEntityUiModules",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LResourceTypeAssemblyName",
                table: "EasyAbpAbpEntityUiModules");

            migrationBuilder.DropColumn(
                name: "LResourceTypeName",
                table: "EasyAbpAbpEntityUiModules");
        }
    }
}
