using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TisCircuitsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFichier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cours_Formation_FormationId",
                        column: x => x.FormationId,
                        principalTable: "Formation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cours_FormationId",
                table: "Cours",
                column: "FormationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cours");
        }
    }
}
