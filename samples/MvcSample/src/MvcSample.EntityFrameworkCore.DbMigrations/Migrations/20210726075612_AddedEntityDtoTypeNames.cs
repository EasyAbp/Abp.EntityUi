using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class AddedEntityDtoTypeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtoTypesAssemblyName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListItemDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "DetailDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "DtoTypesAssemblyName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "EditDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "ListItemDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities");
        }
    }
}
