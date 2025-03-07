using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyNotesChart.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ApplyPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_ApplicationUserId1",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_ApplicationUserId1",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "RefreshTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId1",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ApplicationUserId1",
                table: "RefreshTokens",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_ApplicationUserId1",
                table: "RefreshTokens",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
