using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyNotesChart.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MakeSummaryPropertyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YAxevalues_Integer",
                table: "Charts",
                newName: "YAxevalues_IsInteger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YAxevalues_IsInteger",
                table: "Charts",
                newName: "YAxevalues_Integer");
        }
    }
}
