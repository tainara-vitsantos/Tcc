using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class ProntuarioConfiguration : IEntityTypeConfiguration<ProntuarioModel>
{
    public void Configure(EntityTypeBuilder<ProntuarioModel> builder)
    {
        builder.ToTable("Prontuarios");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.DataAtualizacao);
        builder.Property(x => x.Ativo);
        builder.Property(x => x.PacienteId);
        builder.Property(x => x.NumeroProntuario);
        builder.Property(x => x.DataPrimeiraConsulta);
        builder.Property(x => x.SituacaoProntuario).HasConversion<int>();
        builder.Property(x => x.ObservacoesGerais);

        builder.HasOne(x => x.Paciente)
            .WithOne(x => x.Prontuario)
            .HasForeignKey<ProntuarioModel>(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
