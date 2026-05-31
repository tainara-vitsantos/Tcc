using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class EvolucaoAtendimentoConfiguration : IEntityTypeConfiguration<EvolucaoAtendimentoModel>
{
    public void Configure(EntityTypeBuilder<EvolucaoAtendimentoModel> builder)
    {
        builder.ToTable("EvolucoesAtendimento");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.DataAtualizacao);
        builder.Property(x => x.Ativo);
        builder.Property(x => x.DocumentoClinicoId);
        builder.Property(x => x.AtendimentoId);
        builder.Property(x => x.CriadoPorUsuarioId);
        builder.Property(x => x.DataEvolucao);
        builder.Property(x => x.TextoEvolucao);

        builder.HasOne(x => x.DocumentoClinico)
            .WithMany()
            .HasForeignKey(x => x.DocumentoClinicoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Atendimento)
            .WithMany(x => x.Evolucoes)
            .HasForeignKey(x => x.AtendimentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CriadoPorUsuario)
            .WithMany(x => x.EvolucoesCriadas)
            .HasForeignKey(x => x.CriadoPorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
