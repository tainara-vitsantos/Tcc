using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaEscolaBase.Migrations
{
    /// <inheritdoc />
    public partial class ExpansaoPacienteDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GrauParentescoResponsavel",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrauParentescoResponsavel",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "NomeResponsavel",
                table: "Pacientes");
        }
    }
}
