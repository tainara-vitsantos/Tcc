using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Configurations;

public class DocumentoClinicoConfiguration : IEntityTypeConfiguration<DocumentoClinicoModel>
{
    public void Configure(EntityTypeBuilder<DocumentoClinicoModel> builder)
    {
        builder.ToTable("DocumentosClinicos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.DataAtualizacao);
        builder.Property(x => x.Ativo);
        builder.Property(x => x.ProntuarioId);
        builder.Property(x => x.PacienteId);
        builder.Property(x => x.AtendimentoId);
        builder.Property(x => x.CriadoPorUsuarioId);
        builder.Property(x => x.TipoDocumentoClinico).HasConversion<int>();
        builder.Property(x => x.StatusDocumento).HasConversion<int>();
        builder.Property(x => x.Versao);
        builder.Property(x => x.DataDocumento);
        builder.Property(x => x.FinalizadoEm);
        builder.Property(x => x.Observacoes);
        builder.Property(x => x.ExcluidoLogicamente);

        builder.HasOne(x => x.Prontuario)
            .WithMany(x => x.DocumentosClinicos)
            .HasForeignKey(x => x.ProntuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Paciente)
            .WithMany(x => x.DocumentosClinicos)
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Atendimento)
            .WithMany(x => x.DocumentosClinicos)
            .HasForeignKey(x => x.AtendimentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CriadoPorUsuario)
            .WithMany(x => x.DocumentosCriados)
            .HasForeignKey(x => x.CriadoPorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
