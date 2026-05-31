using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class TermoPsicoterapiaIndividualConfiguration : IEntityTypeConfiguration<TermoPsicoterapiaIndividualModel>
{
    public void Configure(EntityTypeBuilder<TermoPsicoterapiaIndividualModel> builder)
    {
        builder.ToTable("TermosPsicoterapiaIndividual");
        builder.HasKey(x => x.DocumentoClinicoId);

        builder.Property(x => x.DocumentoClinicoId);
        builder.Property(x => x.NomeClienteNoTermo);
        builder.Property(x => x.RGCliente);
        builder.Property(x => x.EstadoCivilCliente);
        builder.Property(x => x.ProfissaoCliente);
        builder.Property(x => x.CidadeCliente);
        builder.Property(x => x.RuaCliente);
        builder.Property(x => x.TelefoneCliente);
        builder.Property(x => x.NomeEstagiarioNoTermo);
        builder.Property(x => x.TelefoneEstagiario);
        builder.Property(x => x.DataAssinatura);
        builder.Property(x => x.DataInicioVigencia);
        builder.Property(x => x.DataFimVigencia);
        builder.Property(x => x.FrequenciaSemanal);
        builder.Property(x => x.DuracaoSessaoMinutos);
        builder.Property(x => x.Abordagem);
        builder.Property(x => x.RegraCancelamento);
        builder.Property(x => x.RegraFaltas);
        builder.Property(x => x.Observacoes);

        builder.HasOne(x => x.DocumentoClinico)
            .WithOne()
            .HasForeignKey<TermoPsicoterapiaIndividualModel>(x => x.DocumentoClinicoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
