using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPiagetDois.Migrations
{
    /// <inheritdoc />
    public partial class adm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdmEscola",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmNomeFuncionario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmFuncaoFuncionario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmMatriculaAluno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmDataCadastroFuncionario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdmDataCadastroAluno = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmEscola", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmEscola");
        }
    }
}
