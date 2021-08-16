using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class ChangedModuleToAggregateRoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "EasyAbpAbpEntityUiModules");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "EasyAbpAbpEntityUiModules");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "EasyAbpAbpEntityUiModules");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "EasyAbpAbpEntityUiModules");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "EasyAbpAbpEntityUiModules",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "EasyAbpAbpEntityUiModules",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "EasyAbpAbpEntityUiModules");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "EasyAbpAbpEntityUiModules");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "EasyAbpAbpEntityUiModules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "EasyAbpAbpEntityUiModules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "EasyAbpAbpEntityUiModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "EasyAbpAbpEntityUiModules",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
