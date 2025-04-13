using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TisCircuitsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNomCompletToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nom_complet",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nom_complet",
                table: "User");
        }
    }
}
