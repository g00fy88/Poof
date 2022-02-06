using Microsoft.EntityFrameworkCore.Migrations;

namespace Poof.DB.Data.Migrations
{
    public partial class removeissuerandapplicantsfromquest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Quests_DbQuestId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_AspNetUsers_IssuerId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_IssuerId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DbQuestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IssuerId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "DbQuestId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssuerId",
                table: "Quests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DbQuestId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quests_IssuerId",
                table: "Quests",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DbQuestId",
                table: "AspNetUsers",
                column: "DbQuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Quests_DbQuestId",
                table: "AspNetUsers",
                column: "DbQuestId",
                principalTable: "Quests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_AspNetUsers_IssuerId",
                table: "Quests",
                column: "IssuerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
