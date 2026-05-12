using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaEscolaBase.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeSupervisorInformado",
                table: "PlantoesPsicologicos",
                newName: "ResponsavelNome");

            migrationBuilder.RenameColumn(
                name: "NomeEstagiarioInformado",
                table: "PlantoesPsicologicos",
                newName: "NomeSupervisor");

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaAluno",
                table: "PlantoesPsicologicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaSupervisor",
                table: "PlantoesPsicologicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtendimento",
                table: "PlantoesPsicologicos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeEstagiario",
                table: "PlantoesPsicologicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Internacao",
                table: "Pacientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MotivoInternacao",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TratamentoCardiologico",
                table: "Pacientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TratamentoNeurologico",
                table: "Pacientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TratamentoPsicologico",
                table: "Pacientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TratamentoPsiquiatrico",
                table: "Pacientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaAluno",
                table: "EvolucoesAtendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssinaturaSupervisor",
                table: "EvolucoesAtendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Atitude",
                table: "AnamnesesAdulto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConteudoPensamento",
                table: "AnamnesesAdulto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PensamentoVelocidade",
                table: "AnamnesesAdulto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sensopercepcao",
                table: "AnamnesesAdulto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TendenciaSuicida",
                table: "AnamnesesAdulto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Escolaridade",
                table: "AnamnesesAdolescente",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssinaturaAluno",
                table: "PlantoesPsicologicos");

            migrationBuilder.DropColumn(
                name: "AssinaturaSupervisor",
                table: "PlantoesPsicologicos");

            migrationBuilder.DropColumn(
                name: "DataAtendimento",
                table: "PlantoesPsicologicos");

            migrationBuilder.DropColumn(
                name: "NomeEstagiario",
                table: "PlantoesPsicologicos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Internacao",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "MotivoInternacao",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "TratamentoCardiologico",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "TratamentoNeurologico",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "TratamentoPsicologico",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "TratamentoPsiquiatrico",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "AssinaturaAluno",
                table: "EvolucoesAtendimento");

            migrationBuilder.DropColumn(
                name: "AssinaturaSupervisor",
                table: "EvolucoesAtendimento");

            migrationBuilder.DropColumn(
                name: "Atitude",
                table: "AnamnesesAdulto");

            migrationBuilder.DropColumn(
                name: "ConteudoPensamento",
                table: "AnamnesesAdulto");

            migrationBuilder.DropColumn(
                name: "PensamentoVelocidade",
                table: "AnamnesesAdulto");

            migrationBuilder.DropColumn(
                name: "Sensopercepcao",
                table: "AnamnesesAdulto");

            migrationBuilder.DropColumn(
                name: "TendenciaSuicida",
                table: "AnamnesesAdulto");

            migrationBuilder.DropColumn(
                name: "Escolaridade",
                table: "AnamnesesAdolescente");

            migrationBuilder.RenameColumn(
                name: "ResponsavelNome",
                table: "PlantoesPsicologicos",
                newName: "NomeSupervisorInformado");

            migrationBuilder.RenameColumn(
                name: "NomeSupervisor",
                table: "PlantoesPsicologicos",
                newName: "NomeEstagiarioInformado");
        }
    }
}
