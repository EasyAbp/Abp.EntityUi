using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class MergedAssemblyProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppServiceAssemblyName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.RenameColumn(
                name: "DtoTypesAssemblyName",
                table: "EasyAbpAbpEntityUiEntities",
                newName: "ContractsAssemblyName");

            migrationBuilder.RenameColumn(
                name: "AppServiceClassName",
                table: "EasyAbpAbpEntityUiEntities",
                newName: "AppServiceInterfaceName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractsAssemblyName",
                table: "EasyAbpAbpEntityUiEntities",
                newName: "DtoTypesAssemblyName");

            migrationBuilder.RenameColumn(
                name: "AppServiceInterfaceName",
                table: "EasyAbpAbpEntityUiEntities",
                newName: "AppServiceClassName");

            migrationBuilder.AddColumn<string>(
                name: "AppServiceAssemblyName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
