using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TisCircuitsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDemandeConge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemandeConges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandeConges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandeConges_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemandeConges_UserId",
                table: "DemandeConges",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandeConges");
        }
    }
}
