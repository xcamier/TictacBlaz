using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tictacApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
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
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentActivityId",
                        column: x => x.ParentActivityId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Characteristics",
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
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristics_Characteristics_ParentCharacteristicId",
                        column: x => x.ParentCharacteristicId,
                        principalTable: "Characteristics",
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
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
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
                        name: "FK_TimeLogs_Categories_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeLogs_Characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeLogs_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeLogsTags",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeLogsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLogsTags", x => new { x.TagsId, x.TimeLogsId });
                    table.ForeignKey(
                        name: "FK_TimeLogsTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeLogsTags_TimeLogs_TimeLogsId",
                        column: x => x.TimeLogsId,
                        principalTable: "TimeLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentActivityId",
                table: "Categories",
                column: "ParentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ParentCharacteristicId",
                table: "Characteristics",
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

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogsTags_TimeLogsId",
                table: "TimeLogsTags",
                column: "TimeLogsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeLogsTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TimeLogs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropTable(
                name: "Objectives");
        }
    }
}
