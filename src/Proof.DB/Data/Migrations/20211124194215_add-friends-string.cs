using Microsoft.EntityFrameworkCore.Migrations;

namespace Poof.DB.Data.Migrations
{
    public partial class addfriendsstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Friends",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friends",
                table: "AspNetUsers");
        }
    }
}
