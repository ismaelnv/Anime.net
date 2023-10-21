using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeWeb.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoLasTablasAnimeYChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Anime",
                newName: "state");

            migrationBuilder.AddColumn<bool>(
                name: "state",
                table: "Capitulo",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "Capitulo");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Anime",
                newName: "status");
        }
    }
}
