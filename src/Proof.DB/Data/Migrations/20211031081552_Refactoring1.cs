using Microsoft.EntityFrameworkCore.Migrations;

namespace Poof.DB.Data.Migrations
{
    public partial class Refactoring1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fellowships_Transactions_DtTransactionId",
                table: "Fellowships");

            migrationBuilder.DropIndex(
                name: "IX_Fellowships_DtTransactionId",
                table: "Fellowships");

            migrationBuilder.DropColumn(
                name: "DtTransactionId",
                table: "Fellowships");

            migrationBuilder.AddColumn<string>(
                name: "DbTransactionId",
                table: "Fellowships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DbTransactionId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fellowships_DbTransactionId",
                table: "Fellowships",
                column: "DbTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DbTransactionId",
                table: "AspNetUsers",
                column: "DbTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Transactions_DbTransactionId",
                table: "AspNetUsers",
                column: "DbTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fellowships_Transactions_DbTransactionId",
                table: "Fellowships",
                column: "DbTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Transactions_DbTransactionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Fellowships_Transactions_DbTransactionId",
                table: "Fellowships");

            migrationBuilder.DropIndex(
                name: "IX_Fellowships_DbTransactionId",
                table: "Fellowships");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DbTransactionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DbTransactionId",
                table: "Fellowships");

            migrationBuilder.DropColumn(
                name: "DbTransactionId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "DtTransactionId",
                table: "Fellowships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fellowships_DtTransactionId",
                table: "Fellowships",
                column: "DtTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fellowships_Transactions_DtTransactionId",
                table: "Fellowships",
                column: "DtTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
