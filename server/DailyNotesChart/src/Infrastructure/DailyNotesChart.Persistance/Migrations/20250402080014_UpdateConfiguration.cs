using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyNotesChart.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartGroups_AspNetUsers_ApplicationUserId",
                table: "ChartGroups");

            migrationBuilder.DropIndex(
                name: "IX_ChartGroups_ApplicationUserId",
                table: "ChartGroups");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ChartGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "ChartGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChartGroups_CreatorId",
                table: "ChartGroups",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartGroups_AspNetUsers_CreatorId",
                table: "ChartGroups",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartGroups_AspNetUsers_CreatorId",
                table: "ChartGroups");

            migrationBuilder.DropIndex(
                name: "IX_ChartGroups_CreatorId",
                table: "ChartGroups");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "ChartGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "ChartGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChartGroups_ApplicationUserId",
                table: "ChartGroups",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartGroups_AspNetUsers_ApplicationUserId",
                table: "ChartGroups",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
