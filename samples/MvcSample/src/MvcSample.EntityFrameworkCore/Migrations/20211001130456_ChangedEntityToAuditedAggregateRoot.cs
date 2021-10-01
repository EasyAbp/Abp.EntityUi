using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class ChangedEntityToAuditedAggregateRoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "EasyAbpAbpEntityUiEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "EasyAbpAbpEntityUiEntities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "EasyAbpAbpEntityUiEntities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "EasyAbpAbpEntityUiEntities",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "EasyAbpAbpEntityUiEntities");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "EasyAbpAbpEntityUiEntities");
        }
    }
}
