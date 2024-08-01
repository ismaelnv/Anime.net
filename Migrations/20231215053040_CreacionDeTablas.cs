using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeWeb.Migrations
{
    public partial class CreacionDeTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    uploadDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    state = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
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
                    table.PrimaryKey("PK_Genre", x => x.id);
                });

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
                    state = table.Column<bool>(type: "INTEGER", nullable: false),
                    image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitulo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Capitulo_Anime_animeId",
                        column: x => x.animeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeModelGenreModel",
                columns: table => new
                {
                    Genresid = table.Column<int>(type: "INTEGER", nullable: false),
                    animesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeModelGenreModel", x => new { x.Genresid, x.animesId });
                    table.ForeignKey(
                        name: "FK_AnimeModelGenreModel_Anime_animesId",
                        column: x => x.animesId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeModelGenreModel_Genre_Genresid",
                        column: x => x.Genresid,
                        principalTable: "Genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    uploadDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    state = table.Column<bool>(type: "INTEGER", nullable: false),
                    animeId = table.Column<int>(type: "INTEGER", nullable: true),
                    chapterId = table.Column<int>(type: "INTEGER", nullable: true),
                    imageType = table.Column<string>(type: "TEXT", nullable: false),
                    imageCategory = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.id);
                    table.ForeignKey(
                        name: "FK_Image_Anime_animeId",
                        column: x => x.animeId,
                        principalTable: "Anime",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_Capitulo_chapterId",
                        column: x => x.chapterId,
                        principalTable: "Capitulo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    url = table.Column<string>(type: "TEXT", nullable: false),
                    language = table.Column<string>(type: "TEXT", nullable: false),
                    uploadDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idChapter = table.Column<int>(type: "INTEGER", nullable: false),
                    state = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.id);
                    table.ForeignKey(
                        name: "FK_Video_Capitulo_idChapter",
                        column: x => x.idChapter,
                        principalTable: "Capitulo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeModelGenreModel_animesId",
                table: "AnimeModelGenreModel",
                column: "animesId");

            migrationBuilder.CreateIndex(
                name: "IX_Capitulo_animeId",
                table: "Capitulo",
                column: "animeId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_animeId",
                table: "Image",
                column: "animeId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_chapterId",
                table: "Image",
                column: "chapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Studio_Anime_Studiosid",
                table: "Studio_Anime",
                column: "Studiosid");

            migrationBuilder.CreateIndex(
                name: "IX_Video_idChapter",
                table: "Video",
                column: "idChapter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeModelGenreModel");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Studio_Anime");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Studio");

            migrationBuilder.DropTable(
                name: "Capitulo");

            migrationBuilder.DropTable(
                name: "Anime");
        }
    }
}
