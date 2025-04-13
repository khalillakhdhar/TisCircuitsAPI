using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TisCircuitsAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixFournitureRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_Fourniture_Fourniture_FournitureId",
                table: "Type_Fourniture");

            migrationBuilder.DropIndex(
                name: "IX_Type_Fourniture_FournitureId",
                table: "Type_Fourniture");

            migrationBuilder.DropColumn(
                name: "FournitureId",
                table: "Type_Fourniture");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Fourniture");

            migrationBuilder.DropColumn(
                name: "details",
                table: "Formation");

            migrationBuilder.AddColumn<int>(
                name: "TypeFournitureId",
                table: "Fourniture",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fourniture_TypeFournitureId",
                table: "Fourniture",
                column: "TypeFournitureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fourniture_Type_Fourniture_TypeFournitureId",
                table: "Fourniture",
                column: "TypeFournitureId",
                principalTable: "Type_Fourniture",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fourniture_Type_Fourniture_TypeFournitureId",
                table: "Fourniture");

            migrationBuilder.DropIndex(
                name: "IX_Fourniture_TypeFournitureId",
                table: "Fourniture");

            migrationBuilder.DropColumn(
                name: "TypeFournitureId",
                table: "Fourniture");

            migrationBuilder.AddColumn<int>(
                name: "FournitureId",
                table: "Type_Fourniture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Fourniture",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "details",
                table: "Formation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Type_Fourniture_FournitureId",
                table: "Type_Fourniture",
                column: "FournitureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Fourniture_Fourniture_FournitureId",
                table: "Type_Fourniture",
                column: "FournitureId",
                principalTable: "Fourniture",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
