using Microsoft.EntityFrameworkCore.Migrations;

namespace Poof.DB.Data.Migrations
{
    public partial class addcontacttoquest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Quests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasContact",
                table: "Quests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "HasContact",
                table: "Quests");
        }
    }
}
