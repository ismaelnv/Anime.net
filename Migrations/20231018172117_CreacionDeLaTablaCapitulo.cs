using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDeLaTablaCapitulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Capitulo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    episode = table.Column<int>(type: "INTEGER", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    uploadDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    animeId = table.Column<int>(type: "INTEGER", nullable: false),
                    animeModelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitulo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Capitulo_Anime_animeModelId",
                        column: x => x.animeModelId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capitulo_animeModelId",
                table: "Capitulo",
                column: "animeModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capitulo");
        }
    }
}
