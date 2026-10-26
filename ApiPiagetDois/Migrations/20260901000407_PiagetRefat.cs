using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPiagetDois.Migrations
{
    /// <inheritdoc />
    public partial class PiagetRefat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlunoTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlunoEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlunoMatricula = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EscolaNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EscolaTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EscolaEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessorNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorDisciplina = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Escola");

            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}
