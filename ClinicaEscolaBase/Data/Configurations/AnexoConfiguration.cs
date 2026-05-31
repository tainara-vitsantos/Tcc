using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class AnexoConfiguration : IEntityTypeConfiguration<AnexoModel>
{
    public void Configure(EntityTypeBuilder<AnexoModel> builder)
    {
        builder.ToTable("Anexos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.DataAtualizacao);
        builder.Property(x => x.Ativo);
        builder.Property(x => x.NomeOriginal);
        builder.Property(x => x.NomeArmazenado);
        builder.Property(x => x.Extensao);
        builder.Property(x => x.MimeType);
        builder.Property(x => x.TamanhoBytes);
        builder.Property(x => x.CaminhoArquivo);
        builder.Property(x => x.HashArquivo);
        builder.Property(x => x.EnviadoPorUsuarioId);
        builder.Property(x => x.DataUpload);
        builder.Property(x => x.IdDocumentoClinico);

        builder.HasOne(x => x.DocumentoClinico)
            .WithMany()
            .HasForeignKey(x => x.IdDocumentoClinico)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EnviadoPorUsuario)
            .WithMany(x => x.AnexosEnviados)
            .HasForeignKey(x => x.EnviadoPorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
