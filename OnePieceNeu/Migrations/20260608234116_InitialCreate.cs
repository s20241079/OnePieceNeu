using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePieceNeu.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fragen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fragentext = table.Column<string>(type: "TEXT", nullable: false),
                    AntwortA = table.Column<string>(type: "TEXT", nullable: false),
                    AntwortB = table.Column<string>(type: "TEXT", nullable: false),
                    AntwortC = table.Column<string>(type: "TEXT", nullable: false),
                    AntwortD = table.Column<string>(type: "TEXT", nullable: false),
                    KorrekteAntwort = table.Column<string>(type: "TEXT", nullable: false),
                    Schwierigkeitsgrad = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fragen", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fragen");
        }
    }
}
