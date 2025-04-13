using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TisCircuitsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitCleanSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemandeEmp",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandeEmp", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    date_creation = table.Column<DateOnly>(type: "date", nullable: false),
                    etat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    details = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Matiere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    DateAjout = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Demande",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    matricule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    datecreation = table.Column<DateOnly>(type: "date", nullable: false),
                    daterecu = table.Column<DateOnly>(type: "date", nullable: true),
                    matriculerecu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    etat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    id_fichier = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Demande__3213E83F4DF6353E", x => x.id);
                    table.ForeignKey(
                        name: "FK__Demande__id_fich__60A75C0F",
                        column: x => x.id_fichier,
                        principalTable: "DemandeEmp",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FormationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.id);
                    table.ForeignKey(
                        name: "FK_Details_Formation_FormationId",
                        column: x => x.FormationId,
                        principalTable: "Formation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fourniture",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    quantite = table.Column<int>(type: "int", nullable: true),
                    date_creation = table.Column<DateOnly>(type: "date", nullable: true),
                    matricule_demandeur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    etats = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MatiereId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fourniture", x => x.id);
                    table.ForeignKey(
                        name: "FK_Fourniture_Matiere",
                        column: x => x.MatiereId,
                        principalTable: "Matiere",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccesFormation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_formation = table.Column<int>(type: "int", nullable: true),
                    Id_service = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesFormation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccesFormation_Formation",
                        column: x => x.Id_formation,
                        principalTable: "Formation",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AccesFormation_Service",
                        column: x => x.Id_service,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    matricule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    matriculeWindows = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    fonction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    responsable = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id_Service = table.Column<int>(type: "int", nullable: true),
                    Etats = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_User_Service_Id_Service",
                        column: x => x.Id_Service,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Type_Fourniture",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    qte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FournitureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_Fourniture", x => x.id);
                    table.ForeignKey(
                        name: "FK_Type_Fourniture_Fourniture_FournitureId",
                        column: x => x.FournitureId,
                        principalTable: "Fourniture",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccesFormation_Id_formation",
                table: "AccesFormation",
                column: "Id_formation");

            migrationBuilder.CreateIndex(
                name: "IX_AccesFormation_Id_service",
                table: "AccesFormation",
                column: "Id_service");

            migrationBuilder.CreateIndex(
                name: "IX_Demande_id_fichier",
                table: "Demande",
                column: "id_fichier");

            migrationBuilder.CreateIndex(
                name: "IX_Details_FormationId",
                table: "Details",
                column: "FormationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fourniture_MatiereId",
                table: "Fourniture",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_Fourniture_FournitureId",
                table: "Type_Fourniture",
                column: "FournitureId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id_Service",
                table: "User",
                column: "Id_Service");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesFormation");

            migrationBuilder.DropTable(
                name: "Demande");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Type_Fourniture");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "DemandeEmp");

            migrationBuilder.DropTable(
                name: "Formation");

            migrationBuilder.DropTable(
                name: "Fourniture");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Matiere");
        }
    }
}
