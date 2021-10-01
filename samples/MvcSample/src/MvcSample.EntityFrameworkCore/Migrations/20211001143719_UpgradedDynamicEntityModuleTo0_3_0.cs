using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class UpgradedDynamicEntityModuleTo0_3_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PermissionSet_AnonymousCreate",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PermissionSet_AnonymousDelete",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PermissionSet_AnonymousGet",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PermissionSet_AnonymousGetList",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PermissionSet_AnonymousUpdate",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionSet_Create",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionSet_Delete",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionSet_Get",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionSet_GetList",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionSet_Manage",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionSet_Update",
                table: "EasyAbpAbpDynamicEntityModelDefinitions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionSet_AnonymousCreate",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_AnonymousDelete",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_AnonymousGet",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_AnonymousGetList",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_AnonymousUpdate",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_Create",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_Delete",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_Get",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_GetList",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_Manage",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");

            migrationBuilder.DropColumn(
                name: "PermissionSet_Update",
                table: "EasyAbpAbpDynamicEntityModelDefinitions");
        }
    }
}
