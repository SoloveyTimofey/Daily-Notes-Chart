using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyNotesChart.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChartGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DefaultNoteTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DefaultChartTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Charts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ChartGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    YAxeValues_Start = table.Column<double>(type: "float", maxLength: 10000, nullable: true),
                    YAxeValues_End = table.Column<double>(type: "float", maxLength: 10000, nullable: true),
                    YAxevalues_Integer = table.Column<bool>(type: "bit", nullable: true),
                    YAxeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charts_ChartGroups_ChartGroupId",
                        column: x => x.ChartGroupId,
                        principalTable: "ChartGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChartGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteTemplates_ChartGroups_ChartGroupId",
                        column: x => x.ChartGroupId,
                        principalTable: "ChartGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(7)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    YAxeValue = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Charts_ChartId",
                        column: x => x.ChartId,
                        principalTable: "Charts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartGroups_DefaultNoteTemplateId",
                table: "ChartGroups",
                column: "DefaultNoteTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Charts_ChartGroupId",
                table: "Charts",
                column: "ChartGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ChartId",
                table: "Notes",
                column: "ChartId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteTemplates_ChartGroupId",
                table: "NoteTemplates",
                column: "ChartGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartGroups_NoteTemplates_DefaultNoteTemplateId",
                table: "ChartGroups",
                column: "DefaultNoteTemplateId",
                principalTable: "NoteTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartGroups_NoteTemplates_DefaultNoteTemplateId",
                table: "ChartGroups");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Charts");

            migrationBuilder.DropTable(
                name: "NoteTemplates");

            migrationBuilder.DropTable(
                name: "ChartGroups");
        }
    }
}
