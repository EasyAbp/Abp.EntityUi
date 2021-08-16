using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSample.Migrations
{
    public partial class FixedBookEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBookDetails_AppBooks_BookId",
                table: "AppBookDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppBookDetails_Id",
                table: "AppBooks",
                column: "Id",
                principalTable: "AppBookDetails",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppBookDetails_Id",
                table: "AppBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBookDetails_AppBooks_BookId",
                table: "AppBookDetails",
                column: "BookId",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
