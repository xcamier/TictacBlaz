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
                name: "CharacteristicsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    IsClosed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicsGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    TargetDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFinalized = table.Column<bool>(type: "INTEGER", nullable: false),
                    FinalizationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objectives_Objectives_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Objectives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFinalized = table.Column<bool>(type: "INTEGER", nullable: false),
                    FinalizationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Projects_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    UseAsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInactive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DefaultGradeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Grades_DefaultGradeId",
                        column: x => x.DefaultGradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    IsClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                    GradeId = table.Column<int>(type: "INTEGER", nullable: true),
                    CharacteristicsGroupId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristics_CharacteristicsGroups_CharacteristicsGroupId",
                        column: x => x.CharacteristicsGroupId,
                        principalTable: "CharacteristicsGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characteristics_Characteristics_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Characteristics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characteristics_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
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
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: true),
                    ObjectiveId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeLogs_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeLogs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ObservationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 510, nullable: false),
                    IsPositive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Evidences = table.Column<string>(type: "TEXT", maxLength: 510, nullable: true),
                    ActorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observations_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeLogsCharacteristics",
                columns: table => new
                {
                    CharacteristicsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeLogsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLogsCharacteristics", x => new { x.CharacteristicsId, x.TimeLogsId });
                    table.ForeignKey(
                        name: "FK_TimeLogsCharacteristics_Characteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeLogsCharacteristics_TimeLogs_TimeLogsId",
                        column: x => x.TimeLogsId,
                        principalTable: "TimeLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "ObservationsCharacteristics",
                columns: table => new
                {
                    CharacteristicsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ObservationsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationsCharacteristics", x => new { x.CharacteristicsId, x.ObservationsId });
                    table.ForeignKey(
                        name: "FK_ObservationsCharacteristics_Characteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObservationsCharacteristics_Observations_ObservationsId",
                        column: x => x.ObservationsId,
                        principalTable: "Observations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservationsTags",
                columns: table => new
                {
                    ObservationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationsTags", x => new { x.ObservationsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ObservationsTags_Observations_ObservationsId",
                        column: x => x.ObservationsId,
                        principalTable: "Observations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObservationsTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_DefaultGradeId",
                table: "Actors",
                column: "DefaultGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CharacteristicsGroupId",
                table: "Characteristics",
                column: "CharacteristicsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_GradeId",
                table: "Characteristics",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ParentId",
                table: "Characteristics",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ParentId",
                table: "Objectives",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_ActorId",
                table: "Observations",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationsCharacteristics_ObservationsId",
                table: "ObservationsCharacteristics",
                column: "ObservationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationsTags_TagsId",
                table: "ObservationsTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ParentId",
                table: "Projects",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_ObjectiveId",
                table: "TimeLogs",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_ProjectId",
                table: "TimeLogs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogsCharacteristics_TimeLogsId",
                table: "TimeLogsCharacteristics",
                column: "TimeLogsId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogsTags_TimeLogsId",
                table: "TimeLogsTags",
                column: "TimeLogsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObservationsCharacteristics");

            migrationBuilder.DropTable(
                name: "ObservationsTags");

            migrationBuilder.DropTable(
                name: "TimeLogsCharacteristics");

            migrationBuilder.DropTable(
                name: "TimeLogsTags");

            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TimeLogs");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "CharacteristicsGroups");

            migrationBuilder.DropTable(
                name: "Objectives");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
