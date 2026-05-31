using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class AtendimentoConfiguration : IEntityTypeConfiguration<AtendimentoModel>
{
    public void Configure(EntityTypeBuilder<AtendimentoModel> builder)
    {
        builder.ToTable("Atendimentos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.DataAtualizacao);
        builder.Property(x => x.Ativo);
        builder.Property(x => x.ProntuarioId);
        builder.Property(x => x.PacienteId);
        builder.Property(x => x.TipoAtendimento).HasConversion<int>();
        builder.Property(x => x.DataHoraInicio);
        builder.Property(x => x.DataHoraFim);
        builder.Property(x => x.StatusAtendimento).HasConversion<int>();
        builder.Property(x => x.FaltaJustificada);
        builder.Property(x => x.Observacoes);

        builder.HasOne(x => x.Prontuario)
            .WithMany(x => x.Atendimentos)
            .HasForeignKey(x => x.ProntuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Paciente)
            .WithMany(x => x.Atendimentos)
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
