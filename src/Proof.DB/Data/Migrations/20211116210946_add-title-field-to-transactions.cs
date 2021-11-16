using Microsoft.EntityFrameworkCore.Migrations;

namespace Poof.DB.Data.Migrations
{
    public partial class addtitlefieldtotransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Transactions");
        }
    }
}
