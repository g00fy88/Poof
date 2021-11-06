using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poof.DB.Data.Migrations
{
    public partial class FrameworkUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "TakeKind",
                table: "Transactions",
                newName: "TakeType");

            migrationBuilder.RenameColumn(
                name: "GiveKind",
                table: "Transactions",
                newName: "TakeSide");

            migrationBuilder.AddColumn<string>(
                name: "GiveSide",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiveType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConsumedTime",
                table: "PersistedGrants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PersistedGrants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "PersistedGrants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeviceCodes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "DeviceCodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeamId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Share = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Memberships_Fellowships_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Fellowships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_OwnerId",
                table: "Memberships",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TeamId",
                table: "Memberships",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "GiveSide",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "GiveType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ConsumedTime",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeviceCodes");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "DeviceCodes");

            migrationBuilder.RenameColumn(
                name: "TakeType",
                table: "Transactions",
                newName: "TakeKind");

            migrationBuilder.RenameColumn(
                name: "TakeSide",
                table: "Transactions",
                newName: "GiveKind");

            migrationBuilder.AddColumn<string>(
                name: "DbTransactionId",
                table: "Fellowships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DbTransactionId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
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
    }
}
