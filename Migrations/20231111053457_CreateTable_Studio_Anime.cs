using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable_Studio_Anime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    uploadDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    state = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Studio_Anime",
                columns: table => new
                {
                    AnimesId = table.Column<int>(type: "INTEGER", nullable: false),
                    Studiosid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio_Anime", x => new { x.AnimesId, x.Studiosid });
                    table.ForeignKey(
                        name: "FK_Studio_Anime_Anime_AnimesId",
                        column: x => x.AnimesId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Studio_Anime_Studio_Studiosid",
                        column: x => x.Studiosid,
                        principalTable: "Studio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studio_Anime_Studiosid",
                table: "Studio_Anime",
                column: "Studiosid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studio_Anime");

            migrationBuilder.DropTable(
                name: "Studio");
        }
    }
}
