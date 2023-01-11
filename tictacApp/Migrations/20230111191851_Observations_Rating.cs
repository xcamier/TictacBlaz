using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tictacApp.Migrations
{
    /// <inheritdoc />
    public partial class ObservationsRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedRating",
                table: "Observations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedRating",
                table: "Observations");
        }
    }
}
