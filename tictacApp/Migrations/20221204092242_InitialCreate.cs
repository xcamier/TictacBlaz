using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tictacApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ParentActivityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_Activity_ParentActivityId",
                        column: x => x.ParentActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Characteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ParentCharacteristicId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristic_Characteristic_ParentCharacteristicId",
                        column: x => x.ParentCharacteristicId,
                        principalTable: "Characteristic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ParentObjectiveId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objectives_Objectives_ParentObjectiveId",
                        column: x => x.ParentObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeSpentInMin = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 140, nullable: true),
                    ActivityId = table.Column<int>(type: "INTEGER", nullable: true),
                    ObjectiveId = table.Column<int>(type: "INTEGER", nullable: true),
                    CharacteristicId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeLogs_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeLogs_Characteristic_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeLogs_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ParentActivityId",
                table: "Activity",
                column: "ParentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristic_ParentCharacteristicId",
                table: "Characteristic",
                column: "ParentCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ParentObjectiveId",
                table: "Objectives",
                column: "ParentObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_ActivityId",
                table: "TimeLogs",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_CharacteristicId",
                table: "TimeLogs",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_ObjectiveId",
                table: "TimeLogs",
                column: "ObjectiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeLogs");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Characteristic");

            migrationBuilder.DropTable(
                name: "Objectives");
        }
    }
}
