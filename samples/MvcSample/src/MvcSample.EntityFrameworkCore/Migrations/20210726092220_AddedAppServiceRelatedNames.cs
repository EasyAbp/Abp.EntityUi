using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class AddedAppServiceRelatedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppServiceAssemblyName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppServiceClassName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppServiceCreateMethodName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppServiceDeleteMethodName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppServiceGetListMethodName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppServiceGetMethodName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppServiceUpdateMethodName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetListInputDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyClassTypeName",
                table: "EasyAbpAbpEntityUiEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppServiceAssemblyName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "AppServiceClassName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "AppServiceCreateMethodName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "AppServiceDeleteMethodName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "AppServiceGetListMethodName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "AppServiceGetMethodName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "AppServiceUpdateMethodName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "GetListInputDtoTypeName",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "KeyClassTypeName",
                table: "EasyAbpAbpEntityUiEntities");
        }
    }
}
