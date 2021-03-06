﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handicap.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HandicapConfigurations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TenantId = table.Column<string>(nullable: true),
                    UpdatePlayersImmediately = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandicapConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchDays",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TenantId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TenantId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Handicap = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TenantId = table.Column<string>(nullable: true),
                    PlayerOneId = table.Column<string>(nullable: true),
                    PlayerTwoId = table.Column<string>(nullable: true),
                    MatchDayId = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    PlayerOneRequiredPoints = table.Column<int>(nullable: false),
                    PlayerOnePoints = table.Column<int>(nullable: false),
                    PlayerTwoRequiredPoints = table.Column<int>(nullable: false),
                    PlayerTwoPoints = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_MatchDays_MatchDayId",
                        column: x => x.MatchDayId,
                        principalTable: "MatchDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchDayPlayers",
                columns: table => new
                {
                    MatchDayId = table.Column<string>(nullable: false),
                    PlayerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDayPlayers", x => new { x.MatchDayId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_MatchDayPlayers_MatchDays_MatchDayId",
                        column: x => x.MatchDayId,
                        principalTable: "MatchDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchDayPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HandicapConfigurations",
                columns: new[] { "Id", "TenantId", "UpdatePlayersImmediately" },
                values: new object[] { "99", "", false });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "Handicap", "LastName", "TenantId" },
                values: new object[,]
                {
                    { "1", "alf", 65, "ralf", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "2", "hans", 35, "maulwurf", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "3", "karl", 30, "klammer", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "4", "bart", 55, "simpson", "816ef7d5-4589-4408-b64c-87594e2075bb" },
                    { "5", "nasen", 25, "baer", "" },
                    { "6", "eier", 5, "kopf", "" },
                    { "7", "rudi", 30, "rakete", "" },
                    { "8", "homer", 55, "simpson", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_MatchDayId",
                table: "Games",
                column: "MatchDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerOneId",
                table: "Games",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerTwoId",
                table: "Games",
                column: "PlayerTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchDayPlayers_PlayerId",
                table: "MatchDayPlayers",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "HandicapConfigurations");

            migrationBuilder.DropTable(
                name: "MatchDayPlayers");

            migrationBuilder.DropTable(
                name: "MatchDays");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
