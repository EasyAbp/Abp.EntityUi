using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class RenamedEntityModuleNameToModuleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityModuleName",
                table: "EasyAbpAbpEntityUiMenuItems",
                newName: "ModuleName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModuleName",
                table: "EasyAbpAbpEntityUiMenuItems",
                newName: "EntityModuleName");
        }
    }
}
