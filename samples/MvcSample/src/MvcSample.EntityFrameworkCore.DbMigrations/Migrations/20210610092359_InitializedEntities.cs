using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class InitializedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpAbpEntityUiEntities",
                columns: table => new
                {
                    ModuleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BelongsTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreationPermission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditEnabled = table.Column<bool>(type: "bit", nullable: false),
                    EditPermission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionEnabled = table.Column<bool>(type: "bit", nullable: false),
                    DeletionPermission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailEnabled = table.Column<bool>(type: "bit", nullable: false),
                    DetailPermission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpAbpEntityUiEntities", x => new { x.ModuleName, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpAbpEntityUiMenuItems",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "EasyAbpAbpEntityUiModules",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpAbpEntityUiModules", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "AppBookDetails",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Outline = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBookDetails", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_AppBookDetails_AppBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "AppBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppBookTags",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBookTags", x => new { x.BookId, x.Tag });
                    table.ForeignKey(
                        name: "FK_AppBookTags_AppBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "AppBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpAbpEntityUiProperties",
                columns: table => new
                {
                    EntityModuleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsEntityCollection = table.Column<bool>(type: "bit", nullable: false),
                    TypeOrEntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowIn_List = table.Column<bool>(type: "bit", nullable: true),
                    ShowIn_Detail = table.Column<bool>(type: "bit", nullable: true),
                    ShowIn_Creation = table.Column<bool>(type: "bit", nullable: true),
                    ShowIn_Edit = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpAbpEntityUiProperties", x => new { x.EntityModuleName, x.EntityName, x.Name });
                    table.ForeignKey(
                        name: "FK_EasyAbpAbpEntityUiProperties_EasyAbpAbpEntityUiEntities_EntityModuleName_EntityName",
                        columns: x => new { x.EntityModuleName, x.EntityName },
                        principalTable: "EasyAbpAbpEntityUiEntities",
                        principalColumns: new[] { "ModuleName", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpAbpEntityUiMenuItems_ParentName",
                table: "EasyAbpAbpEntityUiMenuItems",
                column: "ParentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBookDetails");

            migrationBuilder.DropTable(
                name: "AppBookTags");

            migrationBuilder.DropTable(
                name: "EasyAbpAbpEntityUiMenuItems");

            migrationBuilder.DropTable(
                name: "EasyAbpAbpEntityUiModules");

            migrationBuilder.DropTable(
                name: "EasyAbpAbpEntityUiProperties");

            migrationBuilder.DropTable(
                name: "AppBooks");

            migrationBuilder.DropTable(
                name: "EasyAbpAbpEntityUiEntities");
        }
    }
}
