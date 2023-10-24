using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDeLaTablaVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Anime",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    url = table.Column<string>(type: "TEXT", nullable: false),
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
                name: "IX_Video_idChapter",
                table: "Video",
                column: "idChapter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Anime");
        }
    }
}
