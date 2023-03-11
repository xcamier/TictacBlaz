using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tictacApp.Migrations
{
    /// <inheritdoc />
    public partial class PlannedActivityQuarter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StartupQuarter",
                table: "PlannedActivity",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartupQuarter",
                table: "PlannedActivity");
        }
    }
}
