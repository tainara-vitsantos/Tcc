using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class TermoAutorizacaoMenorConfiguration : IEntityTypeConfiguration<TermoAutorizacaoMenorModel>
{
    public void Configure(EntityTypeBuilder<TermoAutorizacaoMenorModel> builder)
    {
        builder.ToTable("TermosAutorizacaoMenor");
        builder.HasKey(x => x.DocumentoClinicoId);

        builder.Property(x => x.DocumentoClinicoId);
        builder.Property(x => x.InfoFamiliarId);
        builder.Property(x => x.RGResponsavel);
        builder.Property(x => x.CPFResponsavel);
        builder.Property(x => x.NomeMenorNoTermo);
        builder.Property(x => x.DataNascimentoMenorNoTermo);
        builder.Property(x => x.AutorizaAtendimentoPsicologico);
        builder.Property(x => x.AutorizaColetaInformacoes);
        builder.Property(x => x.CienteDevolutivaMensal);
        builder.Property(x => x.DataAssinatura);
        builder.Property(x => x.Observacoes);

        builder.HasOne(x => x.DocumentoClinico)
            .WithOne()
            .HasForeignKey<TermoAutorizacaoMenorModel>(x => x.DocumentoClinicoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.InfoFamiliar)
            .WithMany(x => x.TermosAutorizacaoMenor)
            .HasForeignKey(x => x.InfoFamiliarId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
