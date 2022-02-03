using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poof.DB.Data.Migrations
{
    public partial class addapplyandfinishdatetoquests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApplyDate",
                table: "Quests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "CompletionTime",
                table: "Quests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "Quests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyDate",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "CompletionTime",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "Quests");
        }
    }
}
